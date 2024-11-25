import axios from "axios";
export const login = async (loginRequest) => {
    try{
        var responce = await axios.post("https://localhost:7013/api/auth/login", loginRequest)
        console.log(responce)
        if(responce.data.success){
            let token = responce.data.result.token;
            let cookie =`token=${token}`;
            document.cookie = cookie;
            return true
        }
        throw new Error(responce.data.message)
    }
    catch(ex){
        console.log(ex)
        return false
    }
}