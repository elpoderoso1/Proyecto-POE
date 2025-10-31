using Sistema.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmAsignatura : Form
    {
        public FrmAsignatura()
        {
            InitializeComponent();
        }

        private void FrmAsignatura_Load(object sender, EventArgs e)
        {
            try
            {
                CargarAsignaturas();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar asignaturas: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarAsignaturas()
        {
            dgvAsignaturas.DataSource = NAsignatura.Mostrar();
            dgvAsignaturas.AutoResizeColumns();
            dgvAsignaturas.ClearSelection();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("Debe ingresar el código de la asignatura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Debe ingresar el nombre de la asignatura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDescripcion.Text))
            {
                MessageBox.Show("Debe ingresar una descripción de la asignatura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCreditos.Text))
            {
                MessageBox.Show("Debe ingresar el número de créditos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCreditos.Focus();
                return false;
            }

            if (!int.TryParse(txtCreditos.Text, out int creditos) || creditos <= 0)
            {
                MessageBox.Show("El número de créditos debe ser un valor numérico positivo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCreditos.Focus();
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

                NAsignatura.Agregar(
                    txtCodigo.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    int.Parse(txtCreditos.Text)
                );

                CargarAsignaturas();
                MessageBox.Show("Asignatura registrada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar asignatura: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAsignaturas.CurrentRow == null)
                {
                    MessageBox.Show("Debe seleccionar una asignatura para eliminar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show("¿Está seguro de eliminar esta asignatura?",
                    "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.No)
                    return;

                int id = Convert.ToInt32(dgvAsignaturas.CurrentRow.Cells["IdAsignatura"].Value);
                NAsignatura.Eliminar(id);

                dgvAsignaturas.DataSource = NAsignatura.Mostrar();
                MessageBox.Show("Asignatura eliminada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar la asignatura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtNombre.Clear();
            txtDescripcion.Clear();
            txtCreditos.Clear();
        }

        private int idSeleccionado = 0;

        private void dgvAsignaturas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvAsignaturas.Rows[e.RowIndex];
                idSeleccionado = Convert.ToInt32(fila.Cells["IdAsignatura"].Value);
                txtCodigo.Text = fila.Cells["Codigo"].Value.ToString();
                txtNombre.Text = fila.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = fila.Cells["Descripcion"].Value.ToString();
                txtCreditos.Text = fila.Cells["Creditos"].Value.ToString();
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                if (idSeleccionado == 0)
                {
                    MessageBox.Show("Debe seleccionar una asignatura para editar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidarCampos())
                    return;

                NAsignatura.Editar(
                    idSeleccionado,
                    txtCodigo.Text.Trim(),
                    txtNombre.Text.Trim(),
                    txtDescripcion.Text.Trim(),
                    int.Parse(txtCreditos.Text)
                );

                dgvAsignaturas.DataSource = NAsignatura.Mostrar();
                dgvAsignaturas.AutoResizeColumns();

                MessageBox.Show("Asignatura actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpiarCampos();
                idSeleccionado = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al editar asignatura: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
