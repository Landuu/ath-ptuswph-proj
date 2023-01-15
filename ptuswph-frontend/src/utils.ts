import { PUBLIC_TOKEN } from '$env/static/public';


export const getAuthHeaders = () => {
    const fetchOptions: RequestInit = {
		headers: {
			'Authorization': PUBLIC_TOKEN
		}
	}
    return fetchOptions;
}