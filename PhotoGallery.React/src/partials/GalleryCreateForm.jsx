import React, { useState } from "react";
import {
  Box,
  Button,
  FieldLabel,
  FieldRoot,
  Input,
  Textarea,
} from "@chakra-ui/react";
import { createGallery } from "../scripts/gallery";

const PhotoCreateForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    description: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
      setFormData((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!formData.name || !formData.description) {
      return;
    }
    
    const data = {
        id: 0,
        name: formData.name,
        description: formData.description,
        userId: "",
        Photos: []
    }
    
    const responce = await createGallery(data)

    if(responce.success === true){
      location.replace("http://localhost:5173")
    }
  };

  return (
    <Box maxW="md" mx="auto" mt="10" p="5" borderWidth="1px" borderRadius="lg">
      <form onSubmit={handleSubmit}>
          <FieldRoot>
            <FieldLabel>Title</FieldLabel>
            <Input
              type="text"
              name="name"
              value={formData.title}
              onChange={handleChange}
              placeholder="Enter the title"
            />
          </FieldRoot>
          <FieldRoot>
            <FieldLabel>Description</FieldLabel>
            <Textarea
              name="description"
              value={formData.description}
              onChange={handleChange}
              placeholder="Enter the description"
            />
          </FieldRoot>
        <Button type="submit" colorScheme="teal" width="full">
          Submit
        </Button>
      </form>
    </Box>
  );
};
export default PhotoCreateForm;