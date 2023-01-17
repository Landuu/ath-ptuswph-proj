import store from 'store2';
import { writable } from 'svelte/store';
import type { LoggedUser } from './types';


export const showLoginModal = writable(false);
export const loggedUser = writable<null | LoggedUser>(store.session.get('loggedUser'));
export const loggedUserBalance = writable<null | number>(null);