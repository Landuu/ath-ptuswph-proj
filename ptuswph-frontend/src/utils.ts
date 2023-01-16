import store from 'store2';
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