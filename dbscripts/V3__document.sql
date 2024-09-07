CREATE TABLE public.document(
    id SERIAL PRIMARY KEY,
    name text NOT NULL,
    storage_path text NOT NULL,
    user_id integer NOT NULL REFERENCES dsuser(id),
    organization_id integer REFERENCES organization(id),
    created_by integer NOT NULL,
    date_created timestamp NOT NULL,
    date_modified timestamp NOT NULL,
    modified_by integer NOT NULL
)