# Proyecto Final

>- **Alumno: Paolantonio Diego Gabriel**
>- **Curso: C#**
>- **Comision: 58430**

## Descripcion de la aplicacion

Entregar el proyecto funcionando con los Endpoint con los métodos especificados, para una API que siga las siguientes instrucciones:

### Entidades

**Usuario:**
: Al crear la entidad se genera una por defecto llamada *Admin* declarada en *appsettings.Development.json*

|  | Tipo | Descripcion | Key | Usuario Admin |
|---|---|---|---|---|
| Id | int | Autogenerado | PK | 1 |
| Nombre | string | Maximo de 50 caracteres | | Admin |
| Apellido | string | Maximo de 50 caracteres | | Admin |
| NombreUsuario | string | Maximo de 50 caracteres | | Admin |
| Contraseña | string | Maximo 50 caracteres y minimo 5 caracteres | | CoderHouse |
| Email | string | Maximo de 100 caracteres | | admin@coderhouse.com |

**Producto:**

|  | Tipo | Descripcion | Key |
|---|---|---|---|
| Id | int | Autogenerado | PK |
| Descripcion | string | Maximo 50 caracteres y minimo 5 caracteres |
| PrecioCompra | decimal | Debe ser mayor o igual a 0 |
| PrecioVenta | decimal | Debe ser mayor o igual a 0 |
| Stock | int | Debe ser mayor o igual a 0 |
| TotalProducto | decimal | Debe ser mayor o igual a 0 |
| Categoria | string | Maximo 50 caracteres y minimo 5 caracteres |

**Login:**

|  | Tipo | Descripcion | Key |
|---|---|---|---|
| Id | int | Autogenerado | PK |
| UsuarioId | int | Corresponde al Id del Usuario que se loguea | |
| Nombre | string | Maximo de 50 caracteres, corresponde al Nombre del Usuario logueado | |
| Apellido | string | Maximo de 50 caracteres, corresponde al Apellido del Usuario logueado | |
| NombreUsuario | string | Maximo de 50 caracteres, corresponde al NombreUsuario del Usuario logueado | |
| Email | string | Maximo de 100 caracteres, corresponde al Email del Usuario logueado | |
| FechaLogin | DateTime | Registrada automatica al loguearse | |

**Venta:**

|  | Tipo | Descripcion | Key |
|---|---|---|---|
| Id | int | Autogenerado | PK |
| Usuario | Usuario | Corresponde al Usuario logueado que realiza la venta | FK |
| Producto | List<`Producto`> | Corresponde a cada producto en la venta | |
| Cantidad | List<`int`> | Corresponde a la cantidad de cada producto en la venta | |
| FechaVenta | DateTime | Registrada automatica al realizar la venta | |
| TotalVenta | decimal | Debe ser mayor o igual a 0 | |

**ProductoVendido:**
: Los Productos Vendidos se generan automaticamente al generar una Venta

|  | Tipo | Descripcion | Key |
|---|---|---|---|
| Id | int | Autogenerado | PK |
| Venta | Venta | Corresponde a la Venta del producto | FK |
| Producto | Producto | Corresponde al producto en la venta | FK |
| Cantidad | int | Debe ser mayor o igual a 0 corresponde a la cantidad del producto en la venta | |

### Endpoints

**Usuario:**
| Descripcion | Vervo | Endpoint |
|---|---|---|
| Traer Todos | Get | /api/usuarios |
| Traer Uno | Get | /api/usuarios/`id` |
| Crear | Post | /api/usuarios |
| Modificar | Put | /api/usuarios/`id` |
| Eliminar | Delete | /api/usuarios/`id` |

**Producto:**
| Descripcion | Vervo | Endpoint |
|---|---|---|
| Traer Todos | Get | /api/productos |
| Traer Uno | Get | /api/productos/`id` |
| Crear | Post | /api/productos |
| Modificar | Put | /api/productos/`id` |
| Eliminar | Delete | /api/productos/`id` |

**Login:**
| Descripcion | Vervo | Endpoint |
|---|---|---|
| Traer Todos | Get | /api/login |
| Traer Uno | Get | /api/login/`id` |
| Crear | Get | /api/login |
| Eliminar | Get | /api/login/`id` |

**Venta:**
| Descripcion | Vervo | Endpoint |
|---|---|---|
| Traer Todos | Get | /api/ventas |
| Traer Uno | Get | /api/ventas/`id` |
| Cargar | Post | /api/ventas |
| Eliminar | Delete | /api/ventas/`id` |

**Producto Vendido:**
| Descripcion | Vervo | Endpoint |
|---|---|---|
| Traer Todos | Get | /api/productosvendidos |
| Traer Uno | Get | /api/ventas/`id` |
| Crear | Post | /api/productosvendidos |
| Eliminar | Delete | /api/productosvendidos/`id` |

### Estructura

**SistemaGestionEntities:**
>Es donde estan declaradas todas las entidades

**SistemaGestionData:**
>Es donde esta la gestion para la conexion a la base de datos y el acceso a los datos

**SistemaGestionBusiness:**
>Es donde estan definidos todos los services

**SistemaGestionWebApi:**
>Es donde estan definidos los Controllers para el acceso por los endpoints

**SistemaGestionUI:**
>Es donde esta definida la interfaz grafica con Blazor y la conexion para la navegacion y las consultas a los endpoints
