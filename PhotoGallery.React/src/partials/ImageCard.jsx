import React, { useState } from 'react';
import { Box, Button, Image, Text, Stack, useDisclosure, Input, Textarea, VStack } from '@chakra-ui/react';
import {
  DrawerBackdrop,
  DrawerBody,
  DrawerCloseTrigger,
  DrawerContent,
  DrawerFooter,
  DrawerHeader,
  DrawerRoot,
  DrawerTitle,
  DrawerTrigger,
} from '@chakra-ui/react';
import { getGalleries, createGallery } from '../scripts/gallery';



const ImageCard = ({ image }) => {
  const [newGalleryName, setNewGalleryName] = useState("");
  const [newGalleryDescription, setNewGalleryDescription] = useState("");
  const [galleries, setGalleries] = useState([]);
  const { isOpen, onOpen, onClose } = useDisclosure();

  // Load galleries when the drawer is opened
  const loadGalleries = async () => {
    try {
      const response = await getGalleries(5,1,true);
      if (response.success) {
        setGalleries(response.result); // Assuming `response.result` contains the gallery list
      } else {
        console.error("Failed to fetch galleries");
      }
    } catch (error) {
      console.error("Error fetching galleries:", error);
    }
  };

  // Handle creating a new gallery
  const handleCreateGallery = async () => {
    if (!newGalleryName || !newGalleryDescription) {
      alert("Both name and description are required!");
      return;
    }

    try {
      const response = await createGallery(newGalleryName, newGalleryDescription);
      if (response.success) {
        alert("Gallery created successfully!");
        setNewGalleryName("");
        setNewGalleryDescription("");
        loadGalleries(); // Refresh the gallery list
      } else {
        alert("Failed to create gallery");
      }
    } catch (error) {
      console.error("Error creating gallery:", error);
      alert("Error occurred while creating the gallery.");
    }
  };

  return (
    <Box
      borderWidth={1}
      borderRadius="md"
      overflow="hidden"
      boxShadow="md"
      _hover={{ boxShadow: "lg" }}
      p={4}
    >
      <Image src={image.imgUrl} alt={image.title} boxSize="70%" objectFit="contain" />

      <Stack spacing={3} mt={3}>
        <Text fontSize="xl" fontWeight="bold">
          {image.title}
        </Text>
        <Text>{image.description}</Text>
      </Stack>

      <DrawerRoot>
        <DrawerBackdrop />
        <DrawerTrigger>
          <Button colorScheme={null} onClick={() => {
            onOpen();
            loadGalleries();
            }}>
            Add to Collection
          </Button>
        </DrawerTrigger>
        <DrawerContent>
          <DrawerHeader>Galleries</DrawerHeader>
          <DrawerBody>
            <VStack align="start" spacing={3} mb={6}>
              {galleries.length > 0 ? (
                galleries.map((gallery) => (
                  <Box key={gallery.id} p={3} borderWidth={1} borderRadius="md" width="100%">
                    <Text fontWeight="bold">{gallery.name}</Text>
                    <Text>{gallery.description}</Text>
                  </Box>
                ))
              ) : (
                <Text>No galleries found.</Text>
              )}
            </VStack>
          </DrawerBody>
          <DrawerFooter>
            <DrawerCloseTrigger variant="outline" mr={3} onClick={onClose}>
              Close
            </DrawerCloseTrigger>
          </DrawerFooter>
        </DrawerContent>
      </DrawerRoot>
    </Box>
  );
};

export default ImageCard;