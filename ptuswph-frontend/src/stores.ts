import { writable } from 'svelte/store';
import type { LoggedUser } from './types';

export const showLoginModal = writable(false);
export const loggedUser = writable<null | LoggedUser>(null);