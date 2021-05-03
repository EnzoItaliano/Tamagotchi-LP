import { Pet } from "../entity/Pet";
import { getManager } from "typeorm";

export class PetRepo {

    getAllpets() {
        // get User repository and find all users
        return getManager().getRepository(Pet).find();
    }

    savepet(pet: Pet) {
        return getManager().getRepository(Pet).save(pet);
    }

    deletepet(pet: Pet) {
        return getManager().getRepository(Pet).remove(pet);
    }

    getpetById(petId: number) {
        return getManager().getRepository(Pet).findOne(petId);
    }
}