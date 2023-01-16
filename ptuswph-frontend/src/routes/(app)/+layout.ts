import store from 'store2';
import type { LoggedUser } from "@/types";
import { loggedUser, loggedUserBalance } from "@/stores";
import { getAuthOptions } from "@/utils";
import type { PageLoad } from './$types';


export const load = (async ({fetch}) => {
	loggedUser.subscribe(async (value) => {
		if(value == null) {
			const userdata: LoggedUser = store.session.get('loggedUser');
			if(userdata) {
				loggedUser.set(userdata);
			}
		}
		if(value != null) {
			const res = await fetch('/api/users/wallet', getAuthOptions());
			if(res.status != 200) return;
			const balance = await res.text();
			const balanceNumber = Number(balance.replace(',', '.'));
			loggedUserBalance.set(balanceNumber);
		}
	});


	return { };
}) satisfies PageLoad;