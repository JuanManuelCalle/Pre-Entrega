using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabajoFina.Models;

namespace TrabajoFina
{
    public class Principal
    {
        static void Main(String[] args)
        {
            var ListaUsuario = new List<Usuario>();
            var ListausuarioLogin = new List<Usuario>();
            var ListaProductos = new List<Productos>();
            var ListaProductoVendido = new List<ProductosVendidos>();
            var ListaVenta = new List<Venta>();

            SqlConnectionStringBuilder conn = new();
            conn.DataSource = "(local)";
            conn.InitialCatalog = "SistemaGestion";
            conn.IntegratedSecurity = true;
            var conection = conn.ConnectionString;

            using (SqlConnection cone = new SqlConnection(conection))
            {
                cone.Open();

                bool salir = false;

                while (!salir)
                {

                    try
                    {

                        Console.WriteLine("1. Iniciar Sesion");
                        Console.WriteLine("2. Mi perfil");
                        Console.WriteLine("3. Mis Productos");
                        Console.WriteLine("4. Productos Vendidos");
                        Console.WriteLine("5. Mis ventas");
                        Console.WriteLine("6. Salir");
                        Console.WriteLine("Elige una de las opciones");
                        int opcion = Convert.ToInt32(Console.ReadLine());

                        switch (opcion)
                        {
                            case 1:
                                Console.WriteLine("Ingrese su usuario: ");
                                string NameUser = Console.ReadLine();
                                Console.WriteLine("Ingrese su contraseña: ");
                                string PasswordUser = Console.ReadLine();

                                if (NameUser != "" || PasswordUser != "")
                                {
                                    SqlCommand cmdLogin = cone.CreateCommand();
                                    cmdLogin.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @NombreUser AND Contraseña = @Contrasena";

                                    var paramUser = new SqlParameter("NombreUser", SqlDbType.VarChar);
                                    paramUser.Value = NameUser;

                                    var paramPass = new SqlParameter("Contrasena", SqlDbType.VarChar);
                                    paramPass.Value = PasswordUser;

                                    cmdLogin.Parameters.Add(paramPass);
                                    cmdLogin.Parameters.Add(paramUser);


                                    var readLogin = cmdLogin.ExecuteReader();

                                    while (readLogin.Read())
                                    {
                                        var User = new Usuario();
                                        User.Id = Convert.ToInt32(readLogin.GetValue(0));
                                        User.Nombre = readLogin.GetValue(1).ToString();
                                        User.Apellido = readLogin.GetValue(2).ToString();
                                        User.NombreUsuario = readLogin.GetValue(3).ToString();
                                        User.Mail = readLogin.GetValue(5).ToString();

                                        ListausuarioLogin.Add(User);
                                    }
                                    Console.WriteLine("");
                                    Console.WriteLine("");
                                    Console.WriteLine("----TU LOGUEO ----- ");
                                    foreach (var usuario in ListausuarioLogin)
                                    {
                                        Console.WriteLine("Id Perfil = " + usuario.Id);
                                        Console.WriteLine("Nombre = " + usuario.Nombre);
                                        Console.WriteLine("Apellido = " + usuario.Apellido);
                                        Console.WriteLine("NombreUsuario = " + usuario.NombreUsuario);
                                        Console.WriteLine("Mail = " + usuario.Mail);
                                        Console.WriteLine("--------------------");
                                    }
                                    readLogin.Close();
                                }
                                else
                                {
                                    Console.WriteLine("Ups...Contraseña o Usuario Incorrectos");
                                    Console.WriteLine("");
                                    Console.WriteLine("");
                                }
                                break;

                            case 2:

                                Console.WriteLine("Ingrese su nombre de usuario: ");
                                string NombreUsuario = Console.ReadLine();

                                SqlCommand cmdUser = cone.CreateCommand();
                                cmdUser.CommandText = "SELECT * FROM Usuario WHERE NombreUsuario = @NameUsuario";


                                var paramNombreUsuario = new SqlParameter("NameUsuario", SqlDbType.VarChar);
                                paramNombreUsuario.Value = NombreUsuario;

                                cmdUser.Parameters.Add(paramNombreUsuario);

                                var readUser = cmdUser.ExecuteReader();
                                while (readUser.Read())
                                {
                                    var Usuario = new Usuario();
                                    Usuario.Id = Convert.ToInt32(readUser.GetValue(0));
                                    Usuario.Nombre = readUser.GetValue(1).ToString();
                                    Usuario.Apellido = readUser.GetValue(2).ToString();
                                    Usuario.NombreUsuario = readUser.GetValue(3).ToString();
                                    Usuario.Mail = readUser.GetValue(5).ToString();

                                    ListaUsuario.Add(Usuario);
                                }
                                Console.WriteLine("");
                                Console.WriteLine("----TU INFORMACIÓN ----- ");
                                Console.WriteLine("");
                                foreach (var user in ListaUsuario)
                                {
                                    Console.WriteLine("Id Usuario = " + user.Id);
                                    Console.WriteLine("Nombre = " + user.Nombre);
                                    Console.WriteLine("Apellido = " + user.Apellido);
                                    Console.WriteLine("Nombre Usuario = " + user.NombreUsuario);
                                    Console.WriteLine("Correo = " + user.Mail);
                                    Console.WriteLine("");
                                }
                                readUser.Close();
                                break;

                            case 3:
                                SqlCommand cmd = cone.CreateCommand();
                                cmd.CommandText = "SELECT * FROM PRODUCTO WHERE IdUsuario = @IdUsuario";
                                double IdUsuarioProducto = 1;
                                var parametroProducto = new SqlParameter();
                                parametroProducto.ParameterName = "IdUsuario";
                                parametroProducto.SqlDbType = SqlDbType.BigInt;
                                parametroProducto.Value = IdUsuarioProducto;
                                cmd.Parameters.Add(parametroProducto);

                                var read = cmd.ExecuteReader();
                                while (read.Read())
                                {
                                    var producto = new Productos();
                                    producto.Id = Convert.ToInt32(read.GetValue(0));
                                    producto.Descripciones = read.GetValue(1).ToString();
                                    producto.Costo = Convert.ToDouble(read.GetValue(2));
                                    producto.PrecioVenta = Convert.ToDouble(read.GetValue(3));
                                    producto.Stock = Convert.ToInt32(read.GetValue(4));
                                    producto.IdUsuario = Convert.ToInt32(read.GetValue(5));

                                    ListaProductos.Add(producto);
                                }
                                Console.WriteLine("----TUS PRODUCTOS CARGADOS----- ");
                                Console.WriteLine("");
                                foreach (var producto in ListaProductos)
                                {
                                    Console.WriteLine("Id Producto = " + producto.Id);
                                    Console.WriteLine("Descripcion producto = " + producto.Descripciones);
                                    Console.WriteLine("Costo del producto = " + producto.Costo);
                                    Console.WriteLine("Precio Producto = " + producto.PrecioVenta);
                                    Console.WriteLine("Stock producto = " + producto.Stock);
                                    Console.WriteLine("Id Usuario Registro = " + producto.IdUsuario);
                                    Console.WriteLine("--------------------");
                                }
                                read.Close();
                                break;
                            case 4:
                                Console.WriteLine("Ingrese un id Usuario para ver la venta Los Usuarios que hay(1 - 4): ");
                                double Id = Convert.ToInt64(Console.ReadLine());

                                SqlCommand cmdProducto = cone.CreateCommand();
                                cmdProducto.CommandText = "SELECT p.IdUsuario, v.IdUsuario, pv.IdProducto, pv.IdVenta FROM Producto p INNER JOIN Venta v ON p.Id = v.Id INNER JOIN ProductoVendido pv ON pv.Id = p.Id WHERE p.IdUsuario = @IdUserVenta";

                                var ParamVentasUser = new SqlParameter("IdUserVenta", SqlDbType.VarChar);
                                ParamVentasUser.Value = Id;

                                cmdProducto.Parameters.Add(ParamVentasUser);

                                var readProductoVendido = cmdProducto.ExecuteReader();
                                while (readProductoVendido.Read())
                                {
                                    var ProductoVendido = new ProductosVendidos();
                                    ProductoVendido.Id = Convert.ToInt32(readProductoVendido.GetValue(0));
                                    ProductoVendido.IdUsuarioVenta = Convert.ToInt32(readProductoVendido.GetValue(1));
                                    ProductoVendido.IdProducto = Convert.ToInt32(readProductoVendido.GetValue(2));
                                    ProductoVendido.IdVenta = Convert.ToInt32(readProductoVendido.GetValue(3));

                                    ListaProductoVendido.Add(ProductoVendido);
                                }
                                Console.WriteLine("");
                                Console.WriteLine("");
                                Console.WriteLine("----LISTA Productos Vendidos ----- ");
                                foreach (var productoV in ListaProductoVendido)
                                {
                                    Console.WriteLine("Id del usuario Producto = " + productoV.Id);
                                    Console.WriteLine("Id Usuario Venta = " + productoV.IdUsuarioVenta);
                                    Console.WriteLine("Id Producto = " + productoV.IdProducto);
                                    Console.WriteLine("Id Venta = " + productoV.IdVenta);
                                    Console.WriteLine("--------------------");
                                }
                                readProductoVendido.Close();
                                break;
                            case 5:
                                Console.WriteLine("Ingrese un id Usuario Los Usuarios que hay(1 - 4): ");
                                double IdVentaUsuario = Convert.ToInt64(Console.ReadLine());

                                SqlCommand cmdVenta = cone.CreateCommand();
                                cmdVenta.CommandText = "SELECT * FROM Venta WHERE IdUsuario = @IdUsuarioVenta";
                                var parametroVenta = new SqlParameter();
                                parametroVenta.ParameterName = "IdUsuarioVenta";
                                parametroVenta.SqlDbType = SqlDbType.BigInt;
                                parametroVenta.Value = IdVentaUsuario;
                                cmdVenta.Parameters.Add(parametroVenta);

                                var readVenta = cmdVenta.ExecuteReader();
                                while (readVenta.Read())
                                {
                                    var venta = new Venta();
                                    venta.Id = Convert.ToInt32(readVenta.GetValue(0));
                                    venta.Comentarios = readVenta.GetValue(1).ToString();
                                    venta.IdUsuario = Convert.ToInt32(readVenta.GetValue(2));

                                    ListaVenta.Add(venta);
                                }
                                Console.WriteLine("");
                                Console.WriteLine("----Tus VENTAS ----- ");
                                Console.WriteLine("");
                                foreach (var venta in ListaVenta)
                                {
                                    Console.WriteLine("Id Venta = " + venta.Id);
                                    Console.WriteLine("Comentarios Venta = " + venta.Comentarios);
                                    Console.WriteLine("Id Usuario Venta = " + venta.IdUsuario);
                                    Console.WriteLine("--------------------");
                                }
                                readVenta.Close();
                                break;
                            case 6:
                                Console.WriteLine("Haz salido exitosamente");
                                salir = true;
                                break;
                            default:
                                Console.WriteLine("Elige una opcion entre 1 y 6");
                                break;
                        }

                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }                
            }
        }

    }
}
