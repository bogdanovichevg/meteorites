CREATE TABLE meteorite (
    Id integer PRIMARY KEY,
    Name text,
    NameType text,
    Class text,
    Mass decimal,
    Fall text,
    Year int,
    GeoLocation jsonb
);

/* Проанализировав запросы. Идексы незначительно ускоряют запрос. */
-- EXPLAIN ANALYZE select year, count(name) as count, sum(mass) FROM public.meteorite where year >= 0 and year <= 2000 and class = 'L6' and name Like '%a%' group by year order by count
	
-- CREATE INDEX indexClassIndex ON public.meteorite (class);
-- CREATE INDEX indexYearIndex ON public.meteorite (year);
-- CREATE INDEX indexNameIndex ON public.meteorite (name);

-- CREATE INDEX classYearIndex ON public.meteorite (class, year);
-- CREATE INDEX classNameIndex ON public.meteorite (class, name);

CREATE INDEX classYearNameIndex ON public.meteorite (class, year, name);
-- CREATE INDEX classNameYearIndex ON public.meteorite (class, name, year);
-- CREATE INDEX classYearNameIndex ON public.meteorite (class, year, name);

-- CREATE INDEX it ON public.meteorite (year) WHERE NOT (year >= 0 AND year <= 2000);