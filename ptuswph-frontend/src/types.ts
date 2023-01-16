
export type ApiMovie = {
    id: number,
    title: string,
    release: string,
    category: string,
    description: string,
    rating: string,
    img: string,
    price: number
}

export type LoggedUser = {
    id: number,
    login: string,
    token: string
}
