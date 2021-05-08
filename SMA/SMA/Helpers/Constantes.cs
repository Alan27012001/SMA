using Org.BouncyCastle.Asn1.Mozilla;
using SMA.Modelos;

namespace SMA.Helpers
{
    public class Constantes
    {
        public const string Clase = "Constantes";

        public string linkWeb { get; set; }
        public string linServicios { get; set; }

        public Seguridad seguridad { get; set; }
        public Mensajes mensajes { get; set; }
        public RecuperarContrasena recuperarContrasena { get; set; }
        public Correo correo { get; set; }
        public Proyecto proyecto { get; set; }
        public Motivo motivo { get; set; }
        public Reporte reporte { get; set; }

        #region Mensajes
        public partial class Mensajes
        {
            public string registroGuardado { get; set; }
            public string error { get; set; }
            public string sesionExpirada { get; set; }
        }
        #endregion

        #region Seguridad
        public partial class Seguridad
        {
            public Mensajes mensajes { get; set; }

            public partial class Mensajes
            {
                public string usuarioNoEncontrado { get; set; }
                public string correoVacio { get; set; }
                public string usuarioContrasenaIncorrecto { get; set; }
                public string pantallaNoEncontrado { get; set; }
                public string rolNoEncontrado { get; set; }
                public string correoContrasena { get; set; }
                public string urlNoEncontrado { get; set; }
                public string contrasenaNoValida { get; set; }
            }
        }

        #region Recuperar Contraseña
        public partial class RecuperarContrasena
        {
            public int minutosActivo { get; set; }

            public Correo correo { get; set; }

            public partial class Correo
            {
                public string mensaje = @"<table align = center style ='text-align: center; border: 3px solid #3374FF' width='435' height='300'>
												<br>
													<tr><td><img src='@Ruta/assets/images/gruposikbalLogo.png' style='margin-top:10px;' width='317' height='90' align=center></img></td></tr>
													<tr>
														<td>
															<div style='margin-top:20px;margin-bottom:20px;'>
																<h2 style='font-family: sans-serif;margin: 5px;' align = center>Restablecer Contraseña</h2>
																<p style='font-family: sans-serif;margin: 5px;' align = center>Utiliza el siguiente link para restablecer tu contraseña: </p>
																<table align = center style ='text-align: center' width='280' cellpadding='10' bgcolor='black'>
																	<tr>
																		<td>
																			<span style = 'color: white; font-family: sans-serif;'>Link:</span>
																		</td>
																	</tr>
																	<tr>
																		<td bgcolor = 'white'>
																			<strong style = 'color: #79B729; font-family: sans-serif; font-size: 15px;word-break: break-all;'>@Link</strong>
																		</td>
																	</tr>
																</table>
															</div>
														</td>
													</tr>
												<br>
												</table>";

                public string asunto { get; set; }
            }
        }
        #endregion
        #endregion

        #region Datos de Email Origen
        public partial class Correo
        {
            public string nombreOrigen { get; set; }
            public string correoOrigen { get; set; }
            public string contrasenaOrigen { get; set; }
            public string huesped { get; set; }
            public int puerto { get; set; }
            public bool ssl { get; set; }
        }
        #endregion

        #region Catalogos
        #region Proyectos
        public partial class Proyecto
        {
            public Mensajes mensajes { get; set; }

            public partial class Mensajes
            {
                public string proyectoNoEncontrado { get; set; }
            }
        }
        #endregion
        #region Motivos
        public partial class Motivo
        {
            public Mensajes mensajes { get; set; }

            public partial class Mensajes
            {
                public string motivoNoEncontrado { get; set; }
            }
        }
        #endregion
        #endregion

        #region Reportes
        public partial class Reporte
        {
            public Mensajes mensajes { get; set; }

            public partial class Mensajes
            {
                public string reporteNoEncontrado { get; set; }
            }
        }
        #endregion
    }
}
