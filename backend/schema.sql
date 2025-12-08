-- =============================================================
-- SCHEMA: Challenge Intuit / Yappa - ABM Clientes
-- =============================================================

-- Se elimina la tabla si ya existe
DROP TABLE IF EXISTS clientes;

-- =============================================================
-- CREACIÓN DE TABLA
-- =============================================================
CREATE TABLE clientes (
    id SERIAL PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    apellido VARCHAR(100) NOT NULL,
    razon_social VARCHAR(150) NOT NULL,
    cuit VARCHAR(20) NOT NULL UNIQUE,
    fecha_nacimiento DATE NOT NULL,
    telefono_celular VARCHAR(30) NOT NULL,
    email VARCHAR(150) NOT NULL UNIQUE,
    fecha_creacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    fecha_modificacion TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- =============================================================
-- DATOS DE PRUEBA
-- =============================================================
INSERT INTO clientes (
    nombre, apellido, razon_social, cuit, fecha_nacimiento,
    telefono_celular, email
) VALUES
('Juan', 'Pérez', 'JP Servicios SRL', '20-12345678-9', '1985-06-15', '1165874210', 'juan.perez@example.com'),
('María', 'Gómez', 'MG Soluciones', '27-23456789-0', '1990-09-21', '1165874221', 'maria.gomez@example.com'),
('Carlos', 'López', 'CL Construcciones', '23-34567890-1', '1978-01-10', '1165874332', 'carlos.lopez@example.com'),
('Lucía', 'Martínez', 'LM Consultora', '27-45678901-2', '1992-03-05', '1165874443', 'lucia.martinez@example.com'),
('Diego', 'Fernández', 'DF Diseño', '20-56789012-3', '1988-11-22', '1165874554', 'diego.fernandez@example.com');

CREATE OR REPLACE PROCEDURE actualizar_cliente(
    p_id INTEGER,
    p_nombre VARCHAR(100),
    p_apellido VARCHAR(100),
    p_telefono_celular VARCHAR(30),
    p_email VARCHAR(150)
)
LANGUAGE plpgsql
AS $$
BEGIN
    UPDATE clientes
    SET 
        nombre = p_nombre,
        apellido = p_apellido,
        telefono_celular = p_telefono_celular,
        email = p_email,
        fecha_modificacion = CURRENT_TIMESTAMP 
    WHERE id = p_id;
    
    -- Manejo opcional de errores/transacciones:
    IF NOT FOUND THEN
        -- Si no se encontró ninguna fila con ese ID
        RAISE EXCEPTION 'Cliente con ID % no encontrado.', p_id;
    END IF;

    -- COMMIT se realiza automáticamente al finalizar el PROCEDURE
END;
$$;