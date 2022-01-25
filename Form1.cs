using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pjCaso62
{
    public partial class frmEmpleado : Form
    {
        double tVentas, tMarketing, tLogistica, tPrestamo;
        double aVentas, aMarketing, aLogistica, aPrestamo;
        double aPersonal, aComision;

        public frmEmpleado()
        {
            InitializeComponent();
        }
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (valida() == "")
            {
                string empleado = getEmpleado();
                if (validaEmpleado() == false)
                {
                    int hijos = getHijos();
                    string area = getArea();
                    string condicion = getCondicion();

                    //Realizando calculos
                    int anno = calculaTiempoServicio();
                    double sueldoBase = asiginaSueldoBase(area, condicion);
                    double movilidad = calculaMovilidad(sueldoBase);
                    double asignacion = calculaAsignacion(hijos);
                    double descuento = calculaDescuento(sueldoBase);
                    double neto = calculaNeto(sueldoBase, movilidad, asignacion, descuento);


                    //Imprimir la lista
                    imprimir(anno, sueldoBase, movilidad, asignacion, descuento, neto);
                }
                else
                {
                    MessageBox.Show("Empleado registrado en planilla");
                    return;
                }
            }
            else
            {
                MessageBox.Show("El error se encuentra en " + valida(), "Planilla", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        
    }
    

        private bool validaEmpleado()
        {
            bool estado = false;

            for (int i = 0; i < lvEmpleados.Items.Count; i++)
            {
                string empleado = lvEmpleados.Items[i].SubItems[0].Text;

                if (getEmpleado() == empleado) estado = true;

            }
            return estado;
        }

        /// <summary>
        /// Metodos de captura valor
        /// </summary>
        /// <returns></returns>

        string getEmpleado()
        {
            return txtEmpleado.Text;
        }

        int getHijos()
        {
            return int.Parse(txtHijo.Text);
        }

        string getArea()
        {
            return cboArea.Text;
        }

        string getCondicion()
        {
            return cboCondicion.Text;
        }

        DateTime getFecha()
        {
            return dtFechaIng.Value;

        }

       //Metodo que determina el sueldo
       double asiginaSueldoBase(string area, string condicion)
        {
            switch (area)
            {
                case "Ventas":
                    if (condicion == "Personal")
                        return 2500;
                    else
                        return 500;

                case "Marketing":
                    if (condicion == "Personal")
                        return 1800;
                    else
                        return 1100;

                case "Logistica":
                    if (condicion == "Personal")
                        return 3500;
                    else
                        return 2500;

                case "Prestamo":
                    if (condicion == "Personal")
                        return 1500;
                    else
                        return 990;

            }
            return 0;
        }

        /// <summary>
        /// Calcular anno de servicio
        /// </summary>
        int calculaTiempoServicio()
        {
            return DateTime.Now.Year - getFecha().Year;
        }

        //Calcular la asignacion de movilidad

        double calculaMovilidad(double sueldoBase)
        {
            switch (getArea())
            {
                case "Ventas":
                    if (getCondicion() == "Personal")
                    {
                        return 10.0 / 100 * sueldoBase;
                    }
                    else
                        return 5.0 / 100 * sueldoBase;

                case "Marketing":
                    if (getCondicion() == "Personal")
                    {
                        return 20.0 / 100 * sueldoBase;
                    }
                    else
                        return 10.0 / 100 * sueldoBase;

                case "Logistica":
                    if (getCondicion() == "Personal")
                    {
                        return 30.0 / 100 * sueldoBase;
                    }
                    else
                        return 15.0 / 100 * sueldoBase;

                case "Prestamo":
                    if (getCondicion() == "Personal")
                    {
                        return 25.0 / 100 * sueldoBase;
                    }
                    else
                        return 12.5 / 100 * sueldoBase;
            }
            return 0;
        }

        double calculaAsignacion(int hijos)
        {
            return hijos * 20;
        }

        private void btnConsulta1_Click(object sender, EventArgs e)
        {
            totalEmpeladosxArea();

            lvEstadisticas.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Total de personas del area de ventas";
            elementosFila[1] = tVentas.ToString("0.00");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Total de personas del area de marketing";
            elementosFila[1] = tMarketing.ToString("0.00");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Total de personas del area de logistica";
            elementosFila[1] = tLogistica.ToString("0.00");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Total de personas del area de prestamo";
            elementosFila[1] = tPrestamo.ToString("0.00");
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);


        }

        private void btnConsulta3_Click(object sender, EventArgs e)
        {
            montoAcumuladorxArea();

            lvEstadisticas.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Monto neto acumulado del area de ventas";
            elementosFila[1] = aVentas.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto neto acumulado del area de marketing";
            elementosFila[1] = aMarketing.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto neto acumulado del area de logistica";
            elementosFila[1] = aLogistica.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto neto acumulado del area de prestamo";
            elementosFila[1] = aPrestamo.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);
        }

        private void btnConsulta2_Click(object sender, EventArgs e)
        {
            montoAcumuladorxCondicion();

            lvEstadisticas.Items.Clear();
            string[] elementosFila = new string[2];
            ListViewItem row;

            elementosFila[0] = "Monto neto acumulado por empleado tipo personal";
            elementosFila[1] = aPersonal.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            elementosFila[0] = "Monto neto acumulado por comision tipo personal";
            elementosFila[1] = aComision.ToString();
            row = new ListViewItem(elementosFila);
            lvEstadisticas.Items.Add(row);

            
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        double calculaDescuento(double sueldoBase)
        {
            return 17.0 / 100 * sueldoBase;
        }

        double calculaNeto(double sueldoBase, double movilidad, double asignacion, double descuento)
        {
            return sueldoBase + movilidad + asignacion - descuento;
        }

        //Metodo para imprimir
        void imprimir(int tiempo, double sueldoBase, double movilidad, double asignacion, double descuento, double neto)
        {
            ListViewItem fila = new ListViewItem(getEmpleado());
            fila.SubItems.Add(getHijos().ToString());
            fila.SubItems.Add(getArea());
            fila.SubItems.Add(getCondicion());
            fila.SubItems.Add(tiempo.ToString());
            fila.SubItems.Add(sueldoBase.ToString());
            fila.SubItems.Add(movilidad.ToString());
            fila.SubItems.Add(asignacion.ToString());
            fila.SubItems.Add(descuento.ToString());
            fila.SubItems.Add(neto.ToString());
            lvEmpleados.Items.Add(fila);

        }

        /// <summary>
        /// Metodos para las estadisticas
        /// </summary>
        //Consulta1
        public void totalEmpeladosxArea()
        {
            tVentas = 0;
            tMarketing = 0;
            tLogistica = 0;
            tPrestamo = 0;

            for (int i = 0; i < lvEmpleados.Items.Count; i++)
            {
                string area = lvEmpleados.Items[i].SubItems[2].Text;

                switch (area)
                {
                    case "Ventas": tVentas++; break;
                    case "Marketing": tMarketing++; break;
                    case "Logistica": tLogistica++; break;
                    case "Prestamo": tPrestamo++; break;
                }
            }
        }
        //Consulta2
        public void montoAcumuladorxCondicion()
        {
                aPersonal = 0;
                aComision = 0;

                for (int i = 0; i < lvEmpleados.Items.Count; i++)
                {
                    string condicion = lvEmpleados.Items[i].SubItems[3].Text;
                    switch (condicion)
                    {
                        case "Personal":
                            aPersonal += double.Parse(lvEmpleados.Items[i].SubItems[9].Text);
                            break;

                        case "Comision":
                            aComision += double.Parse(lvEmpleados.Items[i].SubItems[9].Text);
                            break;


                    }
                }

        }

        //Consulta3
        public void montoAcumuladorxArea()
        {
            aVentas = 0;
            aMarketing = 0;
            aLogistica = 0;
            aPrestamo = 0;


            for (int i = 0; i < lvEmpleados.Items.Count; i++)
            {
                string area = lvEmpleados.Items[i].SubItems[2].Text;
                switch (area)
                {
                    case "Ventas":
                        aVentas += double.Parse(lvEmpleados.Items[i].SubItems[9].Text);
                        break;

                    case "Marketing":
                        aMarketing += double.Parse(lvEmpleados.Items[i].SubItems[9].Text);
                        break;

                    case "logistica":
                        aLogistica += double.Parse(lvEmpleados.Items[i].SubItems[9].Text);
                        break;

                    case "Prestamo":
                        aPrestamo += double.Parse(lvEmpleados.Items[i].SubItems[9].Text);
                        break;


                }
            }
            
            ///Metodo valida registros duplicados
                
          
        }

   //Metodo valida datos
   string valida()
        {
            int n;

            if (txtEmpleado.Text.Trim().Length==0)
            {
                txtEmpleado.Focus();
                return "nombre del empleado";

            }
             else if (!Int32.TryParse(txtHijo.Text, out n))
              {
                  txtHijo.Text = "";
                  txtHijo.Focus();
                  return "numero de hijos; no es un valor numerico..";

              }

            else if (cboArea.SelectedIndex==-1)
            {
                cboArea.Focus();
                return "Area de empleado";

            }

            else if (cboCondicion.SelectedIndex==-1)
            {
                cboCondicion.Focus();
                return "condicion de condicion";

            }
            return "";
        }

        
    }
}
