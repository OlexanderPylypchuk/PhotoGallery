import axios from "axios";

export const getPhotos = async (size=5, pageNum=1) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/photo", {
            params: {
                pageSize: size,
                pageNumber: pageNum
            }
        })
        console.log(responce.data)
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const getPhoto = async (id) => {
    try{
        var responce = await axios.get("https://localhost:7013/api/photo/"+id)
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const createPhoto = async (photo) => {
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
        var responce = await axios.post("https://localhost:7013/api/photo/create", photo,
            {
                headers: 
                { 
                    Authorization: `Bearer ${token}`,
                    'Content-Type': `multipart/form-data`
                }  
            }
        )
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const updatePhoto = async (photo) => {
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
            throw new Error("Login to post update photos")
        }
        var responce = await axios.put("https://localhost:7013/api/photo/update", photo,{
            headers: 
            { 
                Authorization: `Bearer ${token}`,
                'Content-Type': `multipart/form-data`
            }  
        }
        )
        
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}

export const deletePhoto = async (id) => {
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
            throw new Error("Login to post delete photos")
        }
        var responce = await axios.delete("https://localhost:7013/api/photo/delete/"+id,
            {
                headers: { Authorization: `Bearer ${token}` }  
            })
        return responce.data
    }
    catch(ex){
        console.log(ex)
    }
}