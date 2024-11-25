import axios from "axios";

export const getGalleries = async (size=5, number=1, ownGalleries = false) => {
    try{
        const parameters = {
            pageSize: size,
            pageNumber: number,
            userGalleries: ownGalleries
        }
        if(ownGalleries){
            const cookies =
                document.cookie.split('; ');
            const cookieMap = {};
            cookies.forEach(cookie => {
                const [name, value] = cookie.split('=');
                cookieMap[name] = value;
            });
            const token = cookieMap['token'];
            if(!token){
                throw new Error("Login to post new photos")
            }
            parameters.Authorization = `Bearer ${token}`
        }
        var responce = await axios.get("https://localhost:7013/api/gallery", parameters)
        console.log(responce)
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const getGallery = async (id) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/gallery/"+id)
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const createGallery = async (gallery) => {
    try{
        const cookies =
                document.cookie.split('; ');
            const cookieMap = {};
            cookies.forEach(cookie => {
                const [name, value] = cookie.split('=');
                cookieMap[name] = value;
            });
            const token = cookieMap['token'];
        if(!token){
            throw new Error("Login to post new photos")
        }
        var responce = await axios.post("https://localhost:7013/api/gallery/create", gallery,{
            headers: 
            { 
                Authorization: `Bearer ${token}`
            }  
        }
        )
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const updateGallery = async (gallery) => {
    try{
        const cookies =
                document.cookie.split('; ');
            const cookieMap = {};
            cookies.forEach(cookie => {
                const [name, value] = cookie.split('=');
                cookieMap[name] = value;
            });
            const token = cookieMap['token'];
        if(!token){
            throw new Error("Login to post new photos")
        }
        var responce = await axios.put("https://localhost:7013/api/gallery/update", gallery,{
            headers: 
            { 
                Authorization: `Bearer ${token}`
            }  
        })
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const deleteGallery = async (id) => {
    try{
        const cookies =
                document.cookie.split('; ');
            const cookieMap = {};
            cookies.forEach(cookie => {
                const [name, value] = cookie.split('=');
                cookieMap[name] = value;
            });
            const token = cookieMap['token'];
        if(!token){
            throw new Error("Login to post new photos")
        }
        var responce = await axios.get("https://localhost:7013/api/gallery/"+id,{
            headers: 
            { 
                Authorization: `Bearer ${token}`
            }  
        }
        )
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}