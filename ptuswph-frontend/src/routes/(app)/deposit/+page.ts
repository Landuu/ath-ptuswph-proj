import { loggedUser } from '@/stores';
import { redirect } from '@sveltejs/kit';
import type { PageLoad } from './$types';


export const load = (() => {
    loggedUser.subscribe(user => {
        if(!user) {
            console.log('!user');
            throw redirect(302, '/');
        }
    })

	return { };
}) satisfies PageLoad;