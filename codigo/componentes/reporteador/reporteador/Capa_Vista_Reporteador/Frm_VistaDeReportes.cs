using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine; // Gerber Asturias
using CrystalDecisions.Shared;               // Gerber Asturias
using Capa_Controlador_Reporteador;

namespace Capa_Vista_Reporteador
{
    public partial class Frm_VistaDeReportes : Form
    {
        Cls_Controlador_Reporteador controlador = new Cls_Controlador_Reporteador();

        public int IdReporte { get; private set; }

        public Frm_VistaDeReportes(int idReporte) : this()
        {
            IdReporte = idReporte;
        }

        public Frm_VistaDeReportes()
        {
            InitializeComponent();
            IdReporte = IdReporte;
            //Asegurar que el CrystalReportViewer se ajuste al formulario
            crystalReportViewer1.Dock = DockStyle.Fill;
        }

        //Inicio de código de: Gerber Asturias con carné: 0901-22-11992 en la fecha 13/09/2025
        //Método para mostrar un reporte en el CrystalReportViewer
        public void MostrarReporte(string ruta)
        {
            try
            {
                if (!File.Exists(ruta))
                {
                    MessageBox.Show("El archivo del reporte no existe: " + ruta, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                ReportDocument reporte = new ReportDocument();
                reporte.Load(ruta);

                // 🔧 Configurar conexión MySQL ODBC (DSN = Prueba1)
                ConnectionInfo connectionInfo = new ConnectionInfo();
                connectionInfo.ServerName = "Prueba1";       // Nombre del DSN
                connectionInfo.DatabaseName = "bd_hoteleria"; // Nombre de la base
                connectionInfo.UserID = "pedro";             // Usuario MySQL
                connectionInfo.Password = "tu_contraseña";   // Contraseña real
                connectionInfo.IntegratedSecurity = false;   // ❗ Importante: usar credenciales, no Windows

                // 🔁 Aplicar conexión a todas las tablas del reporte
                foreach (Table table in reporte.Database.Tables)
                {
                    TableLogOnInfo logonInfo = table.LogOnInfo;
                    logonInfo.ConnectionInfo = connectionInfo;
                    table.ApplyLogOnInfo(logonInfo);
                }

                // 🔁 También aplicar a subreportes (si existen)
                foreach (ReportDocument subreport in reporte.Subreports)
                {
                    foreach (Table table in subreport.Database.Tables)
                    {
                        TableLogOnInfo logonInfo = table.LogOnInfo;
                        logonInfo.ConnectionInfo = connectionInfo;
                        table.ApplyLogOnInfo(logonInfo);
                    }
                }

                // 🧾 Mostrar en el visor
                crystalReportViewer1.ReportSource = reporte;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Fin de código de: Gerber Asturias con carné: 0901-22-11992 en la fecha 13/09/2025


        //Inicio de código de: Gerber Asturias con carné: 0901-22-11992 en la fecha 13/09/2025
        // Método para cargar y mostrar automáticamente el primer reporte
        private void CargarPrimerReporte()
        {
            try
            {
                DataTable dt = controlador.ObtenerReportes();

                if (dt != null && dt.Rows.Count > 0)
                {
                    // Tomar el primer reporte de la tabla
                    string ruta = dt.Rows[0]["Cmp_Ruta_Reporte"].ToString();
                    MostrarReporte(ruta);
                }
                else
                {
                    MessageBox.Show("No hay reportes disponibles.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener reportes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //Fin de código de: Gerber Asturias con carné: 0901-22-11992 en la fecha 13/09/2025


        private void VistaDeReportes_Load(object sender, EventArgs e)
        {
            CargarPrimerReporte();
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
