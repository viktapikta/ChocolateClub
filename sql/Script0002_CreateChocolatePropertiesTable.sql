CREATE TABLE chocolate.properties (
    bar_id                SERIAL CONSTRAINT pk_chocolate_propertieschocolate_bar PRIMARY KEY,
    appearance        decimal NULL,
    aroma      decimal NULL,
    mouth_feel              decimal NULL,
    flavor         decimal NULL,
    quality       decimal NULL,
	date_added timestamptz default (now()) NOT NULL,
	date_modified timestamptz NULL,
    CONSTRAINT pk_chocolate_properties_unique UNIQUE(bar_id)
);