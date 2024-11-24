import axios from "axios";

export const getGalleries = async (size=5, number=1) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/gallery", {
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

export const getGallery = async (id) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/gallery/"+id)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const createGallery = async (gallery) => {
    try{
        var responce = await axios.post("https://localhost:7013/api/gallery/create", gallery)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const updateGallery = async (gallery) => {
    try{
        var responce = await axios.put("https://localhost:7013/api/gallery/update", gallery)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}

export const deleteGallery = async (id) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/gallery/"+id)
        return responce.result
    }
    catch(ex){
        console.log(ex)
    }
}