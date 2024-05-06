--Crea base de datos
CREATE DATABASE subastas
GO

USE SUBASTAS
GO

--Crea esquema
CREATE SCHEMA subastas
GO

--Crea tablas
CREATE TABLE subastas.usuarios (
  id_usuario INT IDENTITY(1,1) NOT NULL,
  nombre_usuario VARCHAR(40) NOT NULL,
  apellido_usuario VARCHAR(40) NOT NULL,
  correo_usuario VARCHAR(40) NOT NULL,
  password VARCHAR(150) NOT NULL,
  esta_activo BIT NOT NULL,
  CONSTRAINT PK_usuarios PRIMARY KEY (id_usuario)
)
GO

CREATE TABLE subastas.cuenta (
  id_cuenta INT IDENTITY(1,1) NOT NULL,
  saldo DECIMAL(8,2) NOT NULL,
  id_usuario INT NOT NULL,
  CONSTRAINT PK_cuenta PRIMARY KEY (id_cuenta)
)
GO

CREATE TABLE subastas.menus (
  id_menu INT IDENTITY(1,1) NOT NULL,
  nombre_menu VARCHAR(40) NOT NULL,
  esta_activo BIT NOT NULL,
  id_menu_padre INT NOT NULL,
  CONSTRAINT PK_menus PRIMARY KEY (id_menu)
)
GO

CREATE TABLE subastas.subastas (
  id_subasta INT IDENTITY(1,1) NOT NULL,
  titulo_subasta VARCHAR(100) NOT NULL,
  monto_inicial DECIMAL(8,2) NOT NULL,
  fecha_subasta DATE NOT NULL,
  esta_activo BIT NOT NULL,
  imagen_subasta VARCHAR(40) NOT NULL,
  CONSTRAINT PK_subastas PRIMARY KEY (id_subasta)
)
GO

CREATE TABLE subastas.participantes_subasta (
  id_subasta INT NOT NULL,
  id_usuario INT NOT NULL,
  esta_activo BIT NOT NULL,
  CONSTRAINT PK_participantes_subasta PRIMARY KEY (id_subasta, id_usuario)
)
GO

CREATE TABLE subastas.permisos (
  id_permiso INT IDENTITY(1,1) NOT NULL,
  nombre_permiso VARCHAR(40) NOT NULL,
  esta_activo BIT NOT NULL,
  id_menu INT NOT NULL,
  CONSTRAINT PK_permisos PRIMARY KEY (id_permiso)
)
GO

CREATE TABLE subastas.pujas (
  id_puja INT IDENTITY(1,1) NOT NULL,
  monto_puja DECIMAL(8,2) NOT NULL,
  fecha_puja DATE NOT NULL,
  id_subasta INT NOT NULL,
  id_usuario INT NOT NULL,
  CONSTRAINT PK_pujas PRIMARY KEY (id_puja)
)
GO

CREATE TABLE subastas.roles (
  id_rol INT IDENTITY(1,1) NOT NULL,
  nombre_rol VARCHAR(40) NOT NULL,
  esta_activo BIT NOT NULL,
  CONSTRAINT PK_roles PRIMARY KEY (id_rol)
)
GO

CREATE TABLE subastas.rol_permiso (
  id_rol INT NOT NULL,
  id_permiso INT NOT NULL,
  esta_activo BIT NOT NULL,
  CONSTRAINT PK_rol_permiso PRIMARY KEY (id_rol, id_permiso)
)
GO

CREATE TABLE subastas.transacciones (
  id_transaccion INT IDENTITY(1,1) NOT NULL,
  monto_transaccion DECIMAL(8,2) NOT NULL,
  fecha_transaccion DATE NOT NULL,
  es_a_favor BIT NOT NULL,
  id_cuenta INT NOT NULL,
  CONSTRAINT PK_transacciones PRIMARY KEY (id_transaccion)
)
GO

CREATE TABLE subastas.usuario_rol (
  id_usuario INT NOT NULL,
  id_rol INT NOT NULL,
  esta_activo BIT NOT NULL,
  CONSTRAINT PK_usuario_rol PRIMARY KEY (id_usuario, id_rol)
)
GO

--Crea Foreign keys
ALTER TABLE subastas.cuenta
  ADD CONSTRAINT FK_cuenta_usuarios
  FOREIGN KEY (id_usuario) REFERENCES subastas.usuarios (id_usuario)
GO

ALTER TABLE subastas.menus
  ADD CONSTRAINT FK_menus_menus
  FOREIGN KEY (id_menu_padre) REFERENCES subastas.menus (id_menu)
GO

ALTER TABLE subastas.participantes_subasta
  ADD CONSTRAINT FK_participantes_subasta_subastas
  FOREIGN KEY (id_subasta) REFERENCES subastas.subastas (id_subasta)
GO

ALTER TABLE subastas.participantes_subasta
  ADD CONSTRAINT FK_participantes_subasta_usuarios
  FOREIGN KEY (id_usuario) REFERENCES subastas.usuarios (id_usuario)
GO

ALTER TABLE subastas.permisos
  ADD CONSTRAINT FK_permisos_menus
  FOREIGN KEY (id_menu) REFERENCES subastas.menus (id_menu)
GO

ALTER TABLE subastas.pujas
  ADD CONSTRAINT FK_pujas_subastas
  FOREIGN KEY (id_subasta) REFERENCES subastas.subastas (id_subasta)
GO

ALTER TABLE subastas.pujas
  ADD CONSTRAINT FK_pujas_usuarios
  FOREIGN KEY (id_usuario) REFERENCES subastas.usuarios (id_usuario)
GO

ALTER TABLE subastas.rol_permiso
  ADD CONSTRAINT FK_rol_permiso_roles
  FOREIGN KEY (id_rol) REFERENCES subastas.roles (id_rol)
GO

ALTER TABLE subastas.rol_permiso
  ADD CONSTRAINT FK_rol_permiso_permisos
  FOREIGN KEY (id_permiso) REFERENCES subastas.permisos (id_permiso)
GO

ALTER TABLE subastas.transacciones
  ADD CONSTRAINT FK_transacciones_cuenta
  FOREIGN KEY (id_cuenta) REFERENCES subastas.cuenta (id_cuenta)
GO

ALTER TABLE subastas.usuario_rol
  ADD CONSTRAINT FK_usuario_rol_usuarios
  FOREIGN KEY (id_usuario) REFERENCES subastas.usuarios (id_usuario)
GO

ALTER TABLE subastas.usuario_rol
  ADD CONSTRAINT FK_usuario_rol_roles
  FOREIGN KEY (id_rol) REFERENCES subastas.roles (id_rol)
GO
