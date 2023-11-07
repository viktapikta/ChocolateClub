CREATE SCHEMA chocolate;
CREATE TABLE chocolate.bar (
    id                SERIAL CONSTRAINT pk_chocolate_bar PRIMARY KEY,
    name        varchar(100) NOT NULL,
    maker      varchar(100) NOT NULL,
    country              varchar(100) NOT NULL,
    type              varchar(100) NOT NULL,
    flavor         varchar(100) NOT NULL,
    source       varchar(100) NOT NULL,
    strain        varchar(100) DEFAULT FALSE,
	more_info        varchar(400) NOT NULL,
	rating        varchar NOT NULL,
	date_added timestamptz default (now()) NOT NULL,
	date_modified timestamptz NULL,
    CONSTRAINT pk_chocolate_bar_unique UNIQUE(id)
);