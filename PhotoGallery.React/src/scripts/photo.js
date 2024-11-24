import axios from "axios";

export const getPhotos = async (size=5, number=1) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/photo", {
            params: {
                pageSize: size,
                pageNumber: number
            }
        })
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const getPhoto = async (id) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/photo/"+id)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const createPhoto = async (photo) => {
    try{
        var responce = await axios.post("https://localhost:7013/api/photo/create", photo)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const updatePhoto = async (photo) => {
    try{
        var responce = await axios.put("https://localhost:7013/api/photo/update", photo)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const deletePhoto = async (id) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/photo/"+id)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}