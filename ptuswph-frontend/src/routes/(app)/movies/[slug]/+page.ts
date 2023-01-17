import { loggedUser, loggedUserBalance } from "@/stores";
import { getAuthOptions } from "@/utils";
import { error } from "@sveltejs/kit";
import type { PageLoad } from "./$types";
	

export const load = (async ({fetch, depends, params}) => {
	depends('movie:view');

    const mid = params.slug
    const res = await fetch(`/api/movies/${mid}`, getAuthOptions());
	if(res.status != 200)
		throw error(res.status, 'Błąd podczas pobierania danych!');
	
	let isLogged = false;
	let isOwned = false;
	loggedUser.subscribe(user => {
		if(user) isLogged = true;
	})
	if(isLogged) {
		const res2 = await fetch(`/api/movies/${mid}/owned`, getAuthOptions());
		if(res.status != 200)
			throw error(res.status, 'Błąd podczas pobierania daynch!')
		isOwned = (await res2.text()).toLowerCase() == 'true';
	}

	const data = await res.json();

	return { movie: data, owned: isOwned };
}) satisfies PageLoad;