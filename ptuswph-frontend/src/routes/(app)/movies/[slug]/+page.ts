import { getAuthOptions } from "@/utils";
import { error } from "@sveltejs/kit";
import type { PageLoad } from "./$types";


export const load = (async ({fetch, params}) => {
    const mid = params.slug
    const res = await fetch(`/api/movies/${mid}`, getAuthOptions());
	if(res.status != 200) {
		throw error(res.status, 'Błąd podczas pobierania danych!');
	}
	const data = res.json();
	return { movie: data };
}) satisfies PageLoad;