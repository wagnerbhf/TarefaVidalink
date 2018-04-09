CREATE TABLE Tarefa (
    Id int IDENTITY(1,1) NOT NULL,
    Titulo varchar(255) NOT NULL,
    Descricao varchar(255),
    DataExecucao datetime,
    CONSTRAINT PK_Tarefa PRIMARY KEY (ID)
); 