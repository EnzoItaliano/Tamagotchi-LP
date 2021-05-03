import { Request, Response } from "express";
import { inspect } from "util";
import { BaseResponse } from "../base-response";
import { Pet } from "../entity/Pet";
import { PetRepo } from "../repository/PetRepo";
import { AccountRepo } from "../repository/AccountRepo";

export let getAllPets = async (req: Request, res: Response) => {
  console.log("GET => GetAllPets");
  let petRepo : PetRepo = new PetRepo();
  let baseResponse : BaseResponse = new BaseResponse();

  try{
    let pets = await petRepo.getAllpets();
    baseResponse.isSuccess = true;
    baseResponse.response = JSON.stringify(pets);
  }
  catch(e){
    console.log(e);
    baseResponse.isSuccess = false;
    baseResponse.response = JSON.stringify(e);
  }

  res.send(baseResponse);
};

export let savePet = async (req: Request, res: Response) => {
  console.log("POST => SavePet");
  //Create the Repo objects
  let petRepo: PetRepo = new PetRepo();
  let accountRepo : AccountRepo = new AccountRepo();
  let petEntity: Pet = new Pet();
  let baseResponse: BaseResponse = new BaseResponse();

  try {
    let pet_req : Pet = req.body;
    petEntity.name = pet_req.name;
    //Get User Entity which is already persisted in DB
    petEntity.account = await accountRepo.getuserById(pet_req.account.id);
    console.log(petEntity.account);
    //Save Pet
    let pet_saved = await petRepo.savepet(petEntity);
    console.log(pet_saved);

    baseResponse.isSuccess = true;
    baseResponse.response = JSON.stringify('success');
  }
  catch (e) {
    console.log(inspect(e));
    baseResponse.isSuccess = false;
    baseResponse.response = JSON.stringify(inspect(e));
  }
  res.send(baseResponse);
}

export let deletePet = async (req: any, res: Response) => {
  console.log("DELETE ==> DeleteOrder");
}