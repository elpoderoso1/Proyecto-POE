using Sistema.Negocio;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmDocente : Form
    {
        public FrmDocente()
        {
            InitializeComponent();
        }

        private void FrmDocente_Load(object sender, EventArgs e)
        {
            try
            {
                dgvDocentes.DataSource = NDocente.Mostrar();
                dgvDocentes.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar docentes: " + ex.Message);
            }
        }

        private void btnCargarFoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Imágenes|*.jpg;*.jpeg;*.png"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                picFoto.Image = Image.FromFile(ofd.FileName);
            }
        }

        private byte[] ImagenToBytes()
        {
            if (picFoto.Image == null) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                picFoto.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre del docente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEspecialidad.Text))
            {
                MessageBox.Show("Debe ingresar la especialidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEspecialidad.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDocumento.Text))
            {
                MessageBox.Show("Debe ingresar el número de documento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDocumento.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtContacto.Text))
            {
                MessageBox.Show("Debe ingresar el contacto del docente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContacto.Focus();
                return false;
            }

            return true;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                NDocente.Agregar(
                    txtNombre.Text.Trim(),
                    txtEspecialidad.Text.Trim(),
                    txtDocumento.Text.Trim(),
                    txtContacto.Text.Trim(),
                    ImagenToBytes()
                );

                dgvDocentes.DataSource = NDocente.Mostrar();
                MessageBox.Show(" Docente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar docente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDocentes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un docente para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = Convert.ToInt32(dgvDocentes.CurrentRow.Cells["IdDocente"].Value);

                DialogResult result = MessageBox.Show("¿Está seguro que desea eliminar este docente?", "Confirmar eliminación",
                                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    NDocente.Borrar(id);
                    dgvDocentes.DataSource = NDocente.Mostrar();
                    MessageBox.Show("Docente eliminado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar docente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtEspecialidad.Clear();
            txtDocumento.Clear();
            txtContacto.Clear();
            picFoto.Image = null;
        }


        private void dgvDocentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtNombre.Text = dgvDocentes.CurrentRow.Cells["Nombre"].Value.ToString();
                txtEspecialidad.Text = dgvDocentes.CurrentRow.Cells["Especialidad"].Value.ToString();
                txtDocumento.Text = dgvDocentes.CurrentRow.Cells["Documento"].Value.ToString();
                txtContacto.Text = dgvDocentes.CurrentRow.Cells["Contacto"].Value.ToString();

                // Mostrar imagen si existe
                if (dgvDocentes.CurrentRow.Cells["Fotografia"].Value != DBNull.Value)
                {
                    byte[] imagen = (byte[])dgvDocentes.CurrentRow.Cells["Fotografia"].Value;
                    using (MemoryStream ms = new MemoryStream(imagen))
                    {
                        picFoto.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picFoto.Image = null;
                }
            }
        }


        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvDocentes.CurrentRow == null)
                {
                    MessageBox.Show("Seleccione un docente para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                int id = Convert.ToInt32(dgvDocentes.CurrentRow.Cells["IdDocente"].Value);

                // Convertir la imagen si existe
                byte[] foto = ImagenToBytes();

        
                NDocente.Editar(
                    id,
                    txtNombre.Text.Trim(),
                    txtEspecialidad.Text.Trim(),
                    txtDocumento.Text.Trim(),
                    txtContacto.Text.Trim(),
                    foto
                );

                dgvDocentes.DataSource = NDocente.Mostrar();
                MessageBox.Show("Docente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar docente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
