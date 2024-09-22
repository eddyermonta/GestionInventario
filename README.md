
# GESTION DE INVENTARIO

Cojunto de apis para la atutenticacion y gestion de inventario


### ENPOINT AUTHCONTROLLER


#### Validar usuario


```http
  POST /api/auth/validate
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `AuthRequest` | `json` | **Required**. correo y contraseña de usuario |

Respuesta: 200 OK con la respuesta de autenticación o 401 Unauthorized.



### ENDPOINTS USUARIO CONTROLLER

#### Obtener usuario por correo electrónico

```http
  GET /api/user/{email}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `email` | `string` | **Required**. A email to search |

Respuesta: 200 OK con el usuario o 404 Not Found.

#### Agregar un nuevo usuario

```http
  GET /api/user
```

Respuesta: 200 OK con la lista de usuarios o 204 No Content.

#### Obtener usuario por correo electrónico

```http
  POST /api/user
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `user` | `json` | **Required**. Data of user to add |

Respuesta: 201 Created con el usuario creado.

#### Actualizar usuario

```http
  PUT /api/user/{email}
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `UserDto` | `json` | **Required**. Data of user to update |

Respuesta: 204 No Content.
