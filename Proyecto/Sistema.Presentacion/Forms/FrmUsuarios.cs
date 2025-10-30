using Sistema.Entidades;
using Sistema.Negocio;
using System;
using System.Windows.Forms;

namespace Sistema.Presentacion.Forms
{
    public partial class FrmUsuarios : Form
    {
        UsuarioNegocio negocio = new UsuarioNegocio();

        public FrmUsuarios()
        {
            InitializeComponent();
            CargarUsuarios();
        }

        private void CargarUsuarios()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = negocio.ListarUsuarios();
        }

        private void FrmUsuarios_Load(object sender, EventArgs e)
        {

        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = new Usuario
                {
                    nombre_usuario = txtNombre.Text,
                    contrasena = txtContrasena.Text,
                    id_rol = int.Parse(txtRol.Text)
                };
                negocio.AgregarUsuario(u);
                CargarUsuarios();
                MessageBox.Show("Usuario agregado correctamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario u = new Usuario
                {
                    id_usuario = int.Parse(txtId.Text),
                    nombre_usuario = txtNombre.Text,
                    contrasena = txtContrasena.Text,
                    id_rol = int.Parse(txtRol.Text)
                };
                negocio.EditarUsuario(u);
                CargarUsuarios();
                MessageBox.Show("Usuario actualizado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);
                negocio.EliminarUsuario(id);
                CargarUsuarios();
                MessageBox.Show("Usuario eliminado.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
