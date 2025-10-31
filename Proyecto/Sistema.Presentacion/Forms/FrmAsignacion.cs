 using Sistema.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema.Presentacion
{
    public partial class FrmAsignacion : Form
    {
        public FrmAsignacion()
        {
            InitializeComponent();
        }

        private void FrmAsignacion_Load(object sender, EventArgs e)
        {
            try
            {
                // 🔹 Cargar Docentes
                cmbDocente.DataSource = NDocente.Mostrar();
                cmbDocente.DisplayMember = "Nombre";
                cmbDocente.ValueMember = "IdDocente";
                cmbDocente.SelectedIndex = -1;

                // 🔹 Cargar Asignaturas
                cmbAsignatura.DataSource = NAsignatura.Mostrar();
                cmbAsignatura.DisplayMember = "Nombre";
                cmbAsignatura.ValueMember = "IdAsignatura";
                cmbAsignatura.SelectedIndex = -1;

                // 🔹 Cargar Grados
                cmbGrado.DataSource = NGrado.Mostrar();
                cmbGrado.DisplayMember = "Nombre";
                cmbGrado.ValueMember = "IdGrado";
                cmbGrado.SelectedIndex = -1;

                // 🔹 Mostrar Asignaciones Existentes
                dgvAsignaciones.DataSource = NAsignacion.Mostrar();
                dgvAsignaciones.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCampos()
        {
            if (cmbDocente.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un docente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDocente.Focus();
                return false;
            }

            if (cmbAsignatura.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar una asignatura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbAsignatura.Focus();
                return false;
            }

            if (cmbGrado.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un grado.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbGrado.Focus();
                return false;
            }

            return true;
        }

        private void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCampos())
                    return;

                int idDocente = Convert.ToInt32(cmbDocente.SelectedValue);
                int idAsignatura = Convert.ToInt32(cmbAsignatura.SelectedValue);
                int idGrado = Convert.ToInt32(cmbGrado.SelectedValue);

                NAsignacion.Asignar(idDocente, idAsignatura, idGrado);

                dgvAsignaciones.DataSource = NAsignacion.Mostrar();
                dgvAsignaciones.AutoResizeColumns();

                MessageBox.Show("Asignación registrada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimpiarCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar la asignación: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            cmbDocente.SelectedIndex = -1;
            cmbAsignatura.SelectedIndex = -1;
            cmbGrado.SelectedIndex = -1;
        }
    }
}
