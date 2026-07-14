CREATE TABLE token (
    idToken INT not null AUTO_INCREMENT,
    token VARCHAR(255) NOT NULL,
    fechaCreacion TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    fechaExpiracion TIMESTAMP NOT NULL,
    idTarjeta INT NOT NULL,
    CONSTRAINT PK_Token PRIMARY KEY (idToken),
    CONSTRAINT FK_Token FOREIGN KEY (idTarjeta) REFERENCES tarjeta (idTarjeta)
) Engine = InnoDB Charset = utf8;	
-- create index idx_token on token(token);
CREATE INDEX idx_token ON token(token);
