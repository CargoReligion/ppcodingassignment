CREATE TABLE public.user(
    id SERIAL PRIMARY KEY,
    email text NOT NULL,
    created_by integer NOT NULL,
    date_created timestamp NOT NULL,
    date_modified timestamp NOT NULL,
    modified_by integer NOT NULL
)