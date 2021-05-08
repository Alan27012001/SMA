//using SMA.Controllers;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.IdentityModel.JsonWebTokens;
//using System.Linq;
//using System.Net;

//namespace SMA.Helpers.Attributes
//{
//	public class AuthorizeAttribute: TypeFilterAttribute
//	{
//		public AuthorizeAttribute(params string[] claim): base(typeof(AuthorizeFilter))
//		{
//			Arguments = new object[] { claim };
//		}
//	}

//	public class AuthorizeFilter: IAuthorizationFilter
//	{
//		readonly string[] _claim;

//		public AuthorizeFilter(params string[] claim)
//		{
//			_claim = claim;
//		}

//		public void OnAuthorization(AuthorizationFilterContext contexto)
//		{
//			var estaAutenticado = contexto.HttpContext.User.Identity.IsAuthenticated;
//			if (estaAutenticado)
//			{
//				var idLogin = contexto.HttpContext.User.Claims.FirstOrDefault(model => model.Type == JwtRegisteredClaimNames.Jti).Value;
//				var usuarioTemp = contexto.HttpContext.User.Claims.FirstOrDefault(model => model.Type == Constantes.Claims.UsuarioId).Value;
//				var idUsuario = int.Parse(usuarioTemp);

//				var controller = new SeguridadController();
//				if (controller.ValidarLogin(idUsuario, idLogin))
//					return;
//				else
//				{
//					contexto.Result = new ContentResult() { Content = "No autorizado para acción. Intente iniciar sesión nuevamente.", StatusCode = (int)HttpStatusCode.Unauthorized };
//					contexto.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//				};
//			}

//			contexto.Result = new ContentResult() { Content = "No autorizado para acción. Intente iniciar sesión nuevamente.", StatusCode = (int)HttpStatusCode.Unauthorized };
//			contexto.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
//		}
//	}
//}
