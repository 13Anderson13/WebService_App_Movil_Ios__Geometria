-- ===== Indicaciones generales =====
CREATE DATABASE geometria;
USE geometria;

-- ===== Tablas del proyecto =====
CREATE TABLE Usuarios(
	id_usuarios INT PRIMARY KEY AUTO_INCREMENT,
    login VARCHAR(50) NOT NULL,
    password VARCHAR(250) NOT NULL,
    nombre VARCHAR(50) NOT NULL,
    apellido VARCHAR(50),
    email VARCHAR(100),
    telefono VARCHAR(20),
    fecha_creacion DATE,
    estado BOOLEAN DEFAULT TRUE
);

CREATE TABLE Categoria_Minijuego(
	id_minijuego INT PRIMARY KEY AUTO_INCREMENT,
    nombre_minijuego VARCHAR(50) NOT NULL,
    icono VARCHAR(50),
    descripcion TEXT,
    estado BOOLEAN DEFAULT TRUE,
    ventana VARCHAR(50) DEFAULT "VentanaDefault"
);

INSERT INTO Categoria_Minijuego (nombre_minijuego, icono, descripcion)VALUES("Relacionar Figuras","heart", "Aprende y relaciona figuras");
INSERT INTO Categoria_Minijuego (nombre_minijuego, icono, descripcion)VALUES("Calcular angulos","triangle", "Aprende y sobre angulos");

CREATE TABLE Logros
(
	id_logro INT PRIMARY KEY AUTO_INCREMENT,
    nombre_logro VARCHAR(50),
    descripcion_logro TEXT,
    icono VARCHAR(50)
);

INSERT INTO Logros (nombre_logro, descripcion_logro, icono) VALUES ("INICIO SESION","Regristrate","circle");

-- ===== Procedimientos almacenados =====

-- Descripcion : (Inserta un usuario en la base de datos, aunque como tal solo en el web service) 
DELIMITER //
CREATE DEFINER=`Bruce`@`%` PROCEDURE `SP_Insertar_Usuario`
(
	IN sp_login varchar(50),
    IN sp_password varchar(250),
    IN sp_nombre varchar(50),
    IN sp_apellido varchar(50),
    IN sp_email varchar(100),
    IN sp_telefono varchar(20)
)
BEGIN
	INSERT INTO geometria.Usuarios
    (
		login,
		password,
        nombre,
        apellido,
        email,
        telefono,
        fecha_creacion,
        estado
    )
    VALUES
    (
		sp_login,
        sp_password,
        sp_nombre,
        sp_apellido,
        sp_email,
        sp_telefono,
        NOW(),
        1
    );
END //
DELIMITER ;

-- Descripcion : (Inserta un nuevo Minijjuego para ser visualizado en el menu de la aplicacion de iOS)
DELIMITER //
CREATE DEFINER = `Bruce`@`%` PROCEDURE `SP_Insertar_Minijuego`
(
	IN sp_nombre_minijuego VARCHAR(50),
    IN sp_icono VARCHAR(50),
    In sp_descripcion TEXT,
    In sp_estado BOOLEAN,
    In sp_ventana VARCHAR(50)
)BEGIN
	INSERT INTO geometria.categoria_minijuego
    (
		nombre_minijuego,
        icono,
        descripcion,
        estado,
        ventana
    )
    VALUES
    (
		sp_nombre_minijuego,
        sp_icono,
        sp_descripcion,
        sp_estado,
        sp_ventana
    );
END
DELIMITER ;


-- Descripcion ()
-- Validar
DELIMITER //
CREATE DEFINER=`Bruce`@`%` PROCEDURE `SP_Validar_Usuario`
(
	IN sp_login VARCHAR(50)
)BEGIN
    SELECT 
		id_usuarios,
        login,
        password,
        nombre,
        apellido
    FROM geometria.Usuarios
    WHERE login = sp_login
    LIMIT 1;
END //
DELIMITER ;

-- ===== VISTAS DISPONIBLES =====
CREATE VIEW V_Consultar_Minijuego
AS
	SELECT id_minijuego, nombre_minijuego, icono, descripcion, estado, ventana from Categoria_Minijuego
END;


CREATE VIEW V_Consultar_Logros
AS
	SELECT id_logro, nombre_logro, descripcion_logro, icono FROM Logros
END;