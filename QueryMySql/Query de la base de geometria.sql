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

INSERT INTO Categoria_Minijuego (nombre_minijuego, icono, descripcion)VALUES("Relacionar Figuras","heart", "Aprende y relaciona figuras",1,"VentanaMatchShadow");
INSERT INTO Categoria_Minijuego (nombre_minijuego, icono, descripcion)VALUES("Calcular angulos","triangle", "Aprende y sobre angulos");

CREATE TABLE Logros
(
	id_logro INT PRIMARY KEY AUTO_INCREMENT,
    nombre_logro VARCHAR(50),
    descripcion_logro TEXT,
    icono VARCHAR(50)
);

INSERT INTO Logros (nombre_logro, descripcion_logro, icono) VALUES ("INICIO SESION","Regristrate","circle");

CREATE TABLE Nivel
(
	id_nivel INT PRIMARY KEY AUTO_INCREMENT,
    numero_nivel LONG,
    id_minijuego INT,
    titulo_nivel VARCHAR(50),
    dificultad INT DEFAULT 0,
    estado BOOLEAN DEFAULT TRUE,
    CONSTRAINT fk_nivel_minijuego FOREIGN KEY (id_minijuego) REFERENCES categoria_minijuego(id_minijuego)
	ON DELETE CASCADE 
    ON UPDATE CASCADE
);

INSERT INTO Nivel (numero_nivel,id_minijuego,titulo_nivel,dificultad) VALUES (1,1, "Nivel 1: Básicos", 1);
INSERT INTO Nivel (numero_nivel,id_minijuego,titulo_nivel,dificultad) VALUES (2,1, "Nivel 2: Básicos", 1);

CREATE TABLE Detalle_Nivel_Figura
(
	id_detalle INT PRIMARY KEY AUTO_INCREMENT,
    id_nivel INT,
	numero_nivel INT,
    nombre_figura VARCHAR(50),
    icono VARCHAR(50) DEFAULT "triangle",
    coordenada_sombra_x FLOAT NOT NULL,
    coordenada_sombra_y FLOAT NOT NULL,
    coordenada_ficha_x FLOAT NOT NULL,
    coordenada_ficha_y FLOAT NOT NULL,
    CONSTRAINT fk_detalle_nivel FOREIGN KEY (id_nivel) REFERENCES Nivel(id_nivel)
    ON DELETE CASCADE 
    ON UPDATE CASCADE
);

INSERT INTO Detalle_Nivel_Figura (id_nivel,numero_nivel, nombre_figura, icono, coordenada_sombra_x, coordenada_sombra_y, coordenada_ficha_x, coordenada_ficha_y) VALUES (1,1, "Estrella", "star.fill", 200, 300, 100, 650);
INSERT INTO Detalle_Nivel_Figura (id_nivel,numero_nivel, nombre_figura, icono, coordenada_sombra_x, coordenada_sombra_y, coordenada_ficha_x, coordenada_ficha_y) VALUES (1,1, "Corazón", "heart.fill", 200, 450, 300, 650);

-- ===== Procedimientos almacenados =====

-- Descripcion : (Inserta un usuario en la base de datos, aunque como tal solo funciona en el web service) 
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

CREATE VIEW v_Consultar_Nivel
AS
	SELECT id_nivel,numero_nivel, id_minijuego, titulo_nivel, dificultad, estado FROM Nivel
END;

CREATE VIEW v_Detalle_Minijuego_matchshadow
AS
	SELECT id_detalle, id_nivel,numero_nivel, nombre_figura, icono, coordenada_sombra_x, coordenada_sombra_y, coordenada_ficha_x, coordenada_ficha_y FROM detalle_nivel_figura
END;
