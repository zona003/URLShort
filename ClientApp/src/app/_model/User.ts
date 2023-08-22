export class User{
    constructor(
        id: number,
        login: string,
        roleId: Role
    ){}

}

enum Role {admin, user}