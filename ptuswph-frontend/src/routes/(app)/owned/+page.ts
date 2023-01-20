import { loggedUser } from '@/stores';
import { getAuthOptions, redirectTo } from '@/utils';
import { error } from '@sveltejs/kit';
import type { PageLoad } from './$types';


export const load = ( async ({ fetch, depends }) => {
    depends('user:owned');

    loggedUser.subscribe(user => {
        if(!user) redirectTo('/');
    })
    const res = await fetch(`/api/users/movies`, getAuthOptions());
	if(res.status != 200)
		throw error(res.status, 'Błąd podczas pobierania danych!');
    const movies = await res.json();

	return { movies };
}) satisfies PageLoad;