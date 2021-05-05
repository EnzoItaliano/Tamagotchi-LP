import { Account } from "../entity/Account";
import { getManager } from "typeorm";

export class AccountRepo {

    getAllusers() {
        // get User repository and find all users
        return getManager().getRepository(Account).find();
    }

    saveuser(user: Account) {
        return getManager().getRepository(Account).save(user);
    }

    deleteuser(user: Account) {
        return getManager().getRepository(Account).remove(user);
    }

    getuserById(userId: number) {
        return getManager().getRepository(Account).findOne(userId);
    }

    getuserByName(userName: string) {
        return getManager().getRepository(Account).find({ where: {username: userName} });
    }
}