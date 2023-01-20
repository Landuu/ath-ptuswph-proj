import { browser } from '$app/environment';
import { goto } from '$app/navigation';
import { redirect } from '@sveltejs/kit';
import store from 'store2';
import { loggedUserBalance } from './stores';
import type { LoggedUser } from './types';


export const getAuthOptions = () => {
	
    const fetchOptions: RequestInit = {
		headers: getAuthHeaders()
	}
    return fetchOptions;
}

export const getAuthHeaders = () => {
	const fetchHeaders: HeadersInit = {
		'Authorization': getAuthToken()
	};
	return fetchHeaders;
}

export const getAuthToken = () => {
	const userdata: LoggedUser = store.session.get('loggedUser');
	return userdata?.token;
}

export const refreshBalance = async () => {
	const res = await fetch('/api/users/wallet', getAuthOptions());
	if(res.status != 200) return;
	const balance = await res.text();
	const balanceNumber = Number(balance.replace(',', '.'));
	loggedUserBalance.set(balanceNumber);
}

export const redirectTo = (loc: string, cond?: boolean) => {
	if(cond == false) return;
	if(browser) goto(loc);
	else throw redirect(302, loc);
}

export const getSessionUser = () => {
	return store.session.get('loggedUser');
}