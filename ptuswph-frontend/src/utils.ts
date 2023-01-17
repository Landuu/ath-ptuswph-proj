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