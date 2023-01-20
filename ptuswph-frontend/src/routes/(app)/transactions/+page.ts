import { loggedUser } from '@/stores';
import { getAuthOptions, redirectTo } from '@/utils';
import { error } from '@sveltejs/kit';
import type { PageLoad } from './$types';


export const load = ( async ({fetch, depends}) => {
    depends('user:transactions');

    loggedUser.subscribe(user => {
        if(!user) redirectTo('/');
    });
    
    const res = await fetch(`/api/users/transactions`, getAuthOptions());
	if(res.status != 200) {
		throw error(res.status, 'Błąd podczas pobierania danych!');
    }
    const transactions = await res.json();

	return { transactions };
}) satisfies PageLoad;