import { PUBLIC_TOKEN } from '$env/static/public';


export const getAuthOptions = () => {
    const fetchOptions: RequestInit = {
		headers: getAuthHeaders()
	}
    return fetchOptions;
}

export const getAuthHeaders = () => {
	const fetchHeaders: HeadersInit = {
		'Authorization': PUBLIC_TOKEN
	};
	return fetchHeaders;
}

export const getAuthToken = () => {
	return PUBLIC_TOKEN;
}