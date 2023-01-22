import { goto } from '$app/navigation';
import { loggedUser } from '@/stores';
import { redirectTo } from '@/utils';
import type { PageLoad } from './$types';


export const load = (({depends}) => {
    depends('register');

    loggedUser.subscribe(user => {
        if(user) redirectTo('/');
    })

	return { };
}) satisfies PageLoad;