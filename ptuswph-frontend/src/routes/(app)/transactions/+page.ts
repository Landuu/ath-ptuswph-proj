import { loggedUser } from '@/stores';
import { getAuthOptions } from '@/utils';
import { error, redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';


export const load = ( async ({ fetch, depends }) => {
    depends('user:transactions');

    loggedUser.subscribe(user => {
        if(!user) {
            console.log('!user');
            throw redirect(302, '/');
        }
    })

    const res = await fetch(`/api/users/transactions`, getAuthOptions());
	if(res.status != 200)
		throw error(res.status, 'Błąd podczas pobierania danych!');
    const transactions = await res.json();

	return { transactions };
}) satisfies PageLoad;