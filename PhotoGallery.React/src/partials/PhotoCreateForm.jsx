import React, { useState } from "react";
import {
  Box,
  Button,
  FieldLabel,
  FieldRoot,
  Input,
  Textarea,
} from "@chakra-ui/react";
import { createPhoto } from "../scripts/photo";

const PhotoCreateForm = () => {
  const [formData, setFormData] = useState({
    title: "",
    description: "",
    image: null,
  });

  const handleChange = (e) => {
    const { name, value, files } = e.target;
    if (name === "image") {
      setFormData((prev) => ({ ...prev, [name]: files[0] }));
    } else {
      setFormData((prev) => ({ ...prev, [name]: value }));
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!formData.title || !formData.description || !formData.image) {
      return;
    }
    const data = new FormData();
    data.append("Id", formData.id || "");
    data.append("Title", formData.title);
    data.append("Description", formData.description);
    data.append("UserId", ""); 
    data.append("Likes", 0); 
    data.append("Dislikes", 0); 
    data.append("Photo", formData.image);
    
    const responce = await createPhoto(data)

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
              name="title"
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
          <FieldRoot>
            <FieldLabel>Image</FieldLabel>
            <Input
              type="file"
              name="image"
              accept="image/*"
              onChange={handleChange}
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