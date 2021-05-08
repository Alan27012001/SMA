using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SMA.Helpers;
using SMA.Modelos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;

namespace SMA.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IConfiguration configuracion;
        protected Constantes constantes;

        public BaseController(IConfiguration iconfig)
        {
            configuracion = iconfig;
            constantes = new Constantes();
            configuracion.GetSection(Constantes.Clase).Bind(constantes);
        }

        protected int ObtenerUsuarioId()
        {
            if (User == null)
                return 0;
            if (User.Claims == null)
                return 0;
            var usuarioTemp = User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.NameId));
            if (usuarioTemp == null)
                return 0;
            return int.Parse(usuarioTemp.Value);
        }

        protected Guid ObtenerLoginId()
        {
            if (User == null)
                return new Guid(string.Empty);
            if (User.Claims == null)
                return new Guid(string.Empty);
            var usuarioTemp = User.Claims.FirstOrDefault(x => x.Type.Equals(JwtRegisteredClaimNames.Jti));
            if (usuarioTemp == null)
                return new Guid(string.Empty);
            return new Guid(usuarioTemp.Value);
        }

        protected HttpStatusCode ValidarLogin()
        {
            var resultado = HttpStatusCode.BadRequest;
            try
            {
                var idUsuario = ObtenerUsuarioId();
                var idLogin = ObtenerLoginId();
                using (var db = new smafacpyaContext())
                {
                    var hoy = DateTime.Now;
                    var logins = db.UsuarioLogin.Where(x => x.IdUsuario == idUsuario && x.FechaVigencia <= hoy && x.Activo);

                    if (logins != null)
                        foreach (var login in logins)
                            login.Activo = false;

                    db.SaveChanges();

                    var vigencia = db.UsuarioLogin.FirstOrDefault(x => x.IdUsuario == idUsuario && x.Id == idLogin && x.Activo);

                    if (vigencia == null)
                        resultado = HttpStatusCode.Unauthorized;
                    else
                        resultado = HttpStatusCode.OK;
                }
            }
            catch (Exception ex)
            {
                resultado = HttpStatusCode.BadRequest;
            }

            return resultado;
        }

        protected string EnviarCorreo(string nombre, string destinatario, string asunto, string mensaje)
        {
            try
            {
                var correo = new MimeMessage();
                var constructor = new BodyBuilder();
                var cliente = new SmtpClient();

                correo.From.Add(new MailboxAddress(constantes.correo.nombreOrigen, constantes.correo.correoOrigen));
                correo.To.Add(new MailboxAddress(nombre, destinatario));
                correo.Subject = asunto;
                constructor.HtmlBody = mensaje;
                correo.Body = constructor.ToMessageBody();

                cliente.Connect(constantes.correo.huesped, constantes.correo.puerto, constantes.correo.ssl);
                cliente.Authenticate(constantes.correo.correoOrigen, constantes.correo.contrasenaOrigen);

                cliente.Send(correo);
                cliente.Disconnect(true);
                cliente.Dispose();
                return null;
            }
            catch (Exception ex)
            {
                return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            }
        }

        protected string Encriptar(string cadenaEncriptar)
        {
            string resultado = string.Empty;
            byte[] encriptado = System.Text.Encoding.Unicode.GetBytes(cadenaEncriptar);
            resultado = Convert.ToBase64String(encriptado);
            return resultado;
        }

        protected string DesEncriptar(string cadenaDesencriptar)
        {
            string resultado = string.Empty;
            byte[] desencriptado = Convert.FromBase64String(cadenaDesencriptar);
            resultado = System.Text.Encoding.Unicode.GetString(desencriptado);
            return resultado;
        }
    }
}
