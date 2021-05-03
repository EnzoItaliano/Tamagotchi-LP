import { Request, Response } from "express";
import { inspect } from 'util';
import { Account } from "../entity/Account";
import { AccountRepo } from "../repository/AccountRepo";
import { BaseResponse } from "../base-response";

export let getAllUsers = (req: Request, res: Response) => {
  console.log("GET => GetAllUsers");
  let userRepo : AccountRepo = new AccountRepo();
  let baseResponse : BaseResponse = new BaseResponse();

  try{
    let users = userRepo.getAllusers();
    users.then(result => {
        baseResponse.isSuccess = true;
        baseResponse.response = JSON.stringify(result);
        res.send(baseResponse);
    })
  }
  catch(e){
    console.log(e);
    baseResponse.isSuccess = false;
    baseResponse.response = JSON.stringify(e);
    res.send(baseResponse)
  }
};

export let getuserById = (req: Request, res: Response) => {
    console.log("GET => Get User By Id");
    let userRepo: AccountRepo = new AccountRepo();
    let baseResponse : BaseResponse = new BaseResponse();

    try {
      let userId = req.body.id;
      let user = userRepo.getuserById(userId);
      user.then(result => {
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

export let saveUser = async (req: Request, res: Response) => {
  console.log("POST => SaveUser");
  let userRepo : AccountRepo = new AccountRepo();
  let userEntity : Account = new Account();
  let baseResponse : BaseResponse = new BaseResponse();

  try{
    userEntity.id = req.body.id;
    userEntity.username = req.body.username;
    userEntity.password = req.body.password;
    let result = await userRepo.saveuser(userEntity);
    console.log(result);
    baseResponse.isSuccess = true;
    baseResponse.response = JSON.stringify('success');
  }
  catch(e){
    console.log(inspect(e));
    baseResponse.isSuccess = false;
    baseResponse.response = JSON.stringify(inspect(e));
  }
  res.send(baseResponse);
}

export let deleteUser = async (req: any, res: Response) => {
  console.log("DELETE ==> DeleteUser");
  let userRepo : AccountRepo = new AccountRepo();
  let userEntity : Account = new Account();
  let baseResponse : BaseResponse = new BaseResponse();

  try {
    let userId = req.body.id;
    let user = userRepo.getuserById(userId);
    user.then(result => {
      userEntity = result;
      let userDelete = userRepo.deleteuser(userEntity);
      userDelete.then(userDeleted => {
        baseResponse.isSuccess = true;
        baseResponse.response = JSON.stringify(userDeleted);
        res.send(baseResponse);
      })
    })
  } catch (e) {
    console.log(e);
    baseResponse.isSuccess = false;
    baseResponse.response = JSON.stringify(e);
    res.send(baseResponse)
  }
}