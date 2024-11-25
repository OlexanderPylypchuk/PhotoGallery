import React, { useState } from "react";

import { login } from "../scripts/authorization";
import { Box, Button, FieldLabel, FieldRoot, Input } from "@chakra-ui/react";

const Login = () => {
    const [formData, setFormData] = useState({
        userName: "",
        password: ""
      });
    
      const handleChange = (e) => {
        const { name, value} = e.target;
        setFormData((prev) => ({ ...prev, [name]: value }));
      };
    
      const handleSubmit = async (e) => {
        e.preventDefault();
        if (!formData.userName || !formData.password) {
            console.log(1)
          return;
        }
        const data = {
          Username: formData.userName,
          Password: formData.password
        };
        
        const result = await login(data)
        if(result === true){
          location.replace("http://localhost:5173")
        }
      };  

    return (
        <Box maxW="md" mx="auto" mt="10" p="5" borderWidth="1px" borderRadius="lg" className="content">
            <form onSubmit={handleSubmit}>
                <FieldRoot>
                    <FieldLabel>UserName</FieldLabel>
                    <Input
                    type="text"
                    name="userName"
                    onChange={handleChange}
                    placeholder="Enter username"
                    />
                </FieldRoot>
                <FieldRoot>
                    <FieldLabel>Password</FieldLabel>
                    <Input
                    type="password"
                    name="password"
                    onChange={handleChange}
                    placeholder="Enter password"
                    />
                </FieldRoot>
                <Button type="submit" colorScheme="teal" width="full">
                Submit
                </Button>
            </form>
        </Box>
    )
}

export default Login