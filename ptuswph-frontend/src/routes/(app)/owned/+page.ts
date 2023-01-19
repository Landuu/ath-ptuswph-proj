import { loggedUser } from '@/stores';
import { getAuthOptions } from '@/utils';
import { error, redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';


export const load = ( async ({ fetch, depends }) => {
    depends('user:owned');

    loggedUser.subscribe(user => {
        if(!user) {
            throw redirect(302, '/');
        }
    });
    const res = await fetch(`/api/users/movies`, getAuthOptions());
	if(res.status != 200)
		throw error(res.status, 'Błąd podczas pobierania danych!');
    const movies = await res.json();

	return { movies };
}) satisfies PageLoad;