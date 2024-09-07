CREATE TABLE public.dsuser(
    id SERIAL PRIMARY KEY,
    email text NOT NULL,
    organization_id integer REFERENCES organization(id),
    created_by integer NOT NULL,
    date_created timestamp NOT NULL,
    date_modified timestamp NOT NULL,
    modified_by integer NOT NULL
)