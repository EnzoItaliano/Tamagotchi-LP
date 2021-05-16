import { Request, Response } from "express";
import { inspect } from "util";
import { BaseResponse } from "../base-response";
import { Pet } from "../entity/Pet";
import { PetRepo } from "../repository/PetRepo";
import { AccountRepo } from "../repository/AccountRepo";

export let getAllPets = async (req: Request, res: Response) => {
  console.log("GET => GetPets");
  let petRepo : PetRepo = new PetRepo();
  let baseResponse : BaseResponse = new BaseResponse();

  try{
    if(req.query.account){
      let pets = await petRepo.getpetsByUserId(req.query.account);
      baseResponse.isSuccess = true;
      baseResponse.response = JSON.stringify(pets);  
    }else{
      let pets = await petRepo.getAllpets();
      baseResponse.isSuccess = true;
      baseResponse.response = JSON.stringify(pets);
    }
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
    petEntity.id = pet_req.id;
    petEntity.outfit = pet_req.outfit;
    petEntity.name = pet_req.name;
    if (!pet_req.happy){
        petEntity.happy = 50;
        petEntity.sad = 50;
        petEntity.hunger = 40;
        petEntity.health = 80;
        petEntity.tired = 40;
        petEntity.sleeping = 40;
        petEntity.dirty = 80;
        petEntity.normal = true;
        petEntity.sick = false;
        petEntity.dead = false;
        petEntity.light = true;
    }else{
        petEntity.happy = pet_req.happy;
        petEntity.sad = pet_req.sad;
        petEntity.hunger = pet_req.hunger;
        petEntity.health = pet_req.health;
        petEntity.tired = pet_req.tired;
        petEntity.sleeping = pet_req.sleeping;
        petEntity.dirty = pet_req.dirty;
        petEntity.normal = pet_req.normal;
        petEntity.sick = pet_req.sick;
        petEntity.dead = pet_req.dead;
        petEntity.light = pet_req.light;
    }

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var HH = String(today.getHours()).padStart(2, '0');
    var MM = String(today.getMinutes()).padStart(2, '0');
    var SS = String(today.getSeconds()).padStart(2, '0');
    var time = `${dd}/${mm}/${yyyy} ${HH}:${MM}:${SS}`;
    petEntity.time = time;
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

export let getpetById = (req: Request, res: Response) => {
  console.log("GET => Get Pet By Id");
  let petRepo : PetRepo = new PetRepo();
  let baseResponse : BaseResponse = new BaseResponse();
  try {
    let petId = req.params.id;
    let pet = petRepo.getpetById(petId);
    pet.then(result => {
      baseResponse.isSuccess = true;
      baseResponse.response = JSON.stringify(result);
      res.send(baseResponse);
    })
  } catch (e) {
    console.log(e);
    baseResponse.isSuccess = false;
    baseResponse.response = JSON.stringify(e);
    res.send(baseResponse)
  }
}

export let deletePet = async (req: any, res: Response) => {
  console.log("DELETE ==> DeleteOrder");
}
