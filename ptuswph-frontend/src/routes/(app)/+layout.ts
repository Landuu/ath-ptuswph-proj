import store from 'store2';
import type { LoggedUser } from "@/types";
import { loggedUser, loggedUserBalance } from "@/stores";
import { getAuthOptions, refreshBalance } from "@/utils";
import type { PageLoad } from './$types';

export const ssr = false;

export const load: any = (async ({fetch, depends}) => {
	depends('user:wallet');

	let isLogged = false;
	loggedUser.subscribe(user => {
		if(user) isLogged = true;
	});
	
	if(isLogged) {
		const res = await fetch('/api/users/wallet', getAuthOptions());
		if(res.status != 200) return;
		const balance = await res.text();
		const balanceNumber = Number(balance.replace(',', '.'));
		loggedUserBalance.set(balanceNumber);
	}

	return { };
}) satisfies PageLoad;