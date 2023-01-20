import { loggedUser } from '@/stores';
import { redirectTo } from '@/utils';
import type { PageLoad } from './$types';


export const load = (() => {
    loggedUser.subscribe(user => {
        if(!user) redirectTo('/');
    })

	return { };
}) satisfies PageLoad;