import os
import random, pygame, sys
from pygame.locals import *

if not pygame.font: print('Warning, fonts disabled')
if not pygame.mixer: print('Warning, sound disabled')

FPS = 60
WINDOWWIDTH = 600
WINDOWHEIGHT = 600

UP = 'up'
DOWN = 'down'
LEFT = 'left'
RIGHT = 'right'
NONE = 'none'

#          R    G B
WHITE = (255, 255, 255)
BLACK = ( 0, 0, 0)
RED = (255, 0, 0)
GREEN = ( 0, 255, 0)
DARKGREEN = ( 0, 155, 0)
DARKGRAY = ( 40, 40, 40)
BGCOLOR = BLACK

def load_image(name, colorkey=None):
    fullname = os.path.join('data', name)
    try:
        image = pygame.image.load(fullname)
    except pygame.error as message:
        print('Cannot load image:', fullname)
        raise SystemExit(message)
    image = image.convert()
    if colorkey is not None:
        if colorkey is -1:
            colorkey = image.get_at((0, 0))
        image.set_colorkey(colorkey, RLEACCEL)
    return image, image.get_rect()

class ShipBullet(pygame.sprite.Sprite):

    def __init__(self):
        pygame.sprite.Sprite.__init__(self)  # call Sprite intializer
        self.image, self.rect = load_image('bullet.png', -1)
        screen = pygame.display.get_surface()
        self.area = screen.get_rect()
        self.rect.topleft = 10, 400
        self.x = 0
        self.y = 0


class Ship(pygame.sprite.Sprite):

    def __init__(self):
        pygame.sprite.Sprite.__init__(self)  # call Sprite intializer
        self.image, self.rect = load_image('fighter.gif', -1)
        screen = pygame.display.get_surface()
        self.area = screen.get_rect()
        self.rect.topleft = 10, 400
        self.move = 0
        self.dizzy = 0

    def update(self):
        # self.image = pygame.transform.flip(self.image, 1, 0)
        # print(self.rect.left)
        # print (self.area.right)

        if DISPLAYSURF.get_rect().contains(self):
            newpos = self.rect.move((self.move, 0))
        else:
        #     DISPLAYSURF.clamp(self.get_rect())
        # We need to refactor this later
            if self.rect.left < 0:
                newpos = self.rect.move((1, 0))
            elif self.rect.right > 500:
                newpos = self.rect.move((-1, 0))

        self.image = pygame.transform.flip(self.image, 1, 0)
        self.rect = newpos

        # print (newpos)

def main():
    """this function is called when the program starts.
       it initializes everything it needs, then runs in
       a loop until the function returns."""
    # Initialize Everything
    global FPSCLOCK, DISPLAYSURF, BASICFONT
    direction = NONE

    pygame.init()
    FPSCLOCK = pygame.time.Clock()
    DISPLAYSURF = pygame.display.set_mode((WINDOWWIDTH, WINDOWHEIGHT))
    BASICFONT = pygame.font.Font('freesansbold.ttf', 18)
    pygame.display.set_caption('Space War')
    pygame.mouse.set_visible(1)

    # Prepare Game Objects
    #clock = pygame.time.Clock()
    ship = Ship()
    allsprites = pygame.sprite.RenderPlain((ship))

    while True: # main game loop
        for event in pygame.event.get(): # event handling loop
            if event.type == QUIT:
                return
            elif event.type == KEYDOWN:
                if (event.key == K_LEFT or event.key == K_a):
                    direction = LEFT
                elif (event.key == K_RIGHT or event.key == K_d):
                    direction = RIGHT
                elif (event.key == K_UP or event.key):
                    direction = UP
                elif event.key == K_ESCAPE:
                    return
            elif event.type == KEYUP:
                direction = NONE

            if direction == LEFT:
                # print ("here")
                ship.move = -2
            elif direction == RIGHT:
                ship.move = 2
            elif direction == UP:
                print (ship.rect.center)
            else:
                ship.move = 0

            # print (direction)
        # Display The Background
        DISPLAYSURF.fill(BGCOLOR)
        allsprites.update()

        allsprites.draw(DISPLAYSURF)
        pygame.display.update()

        FPSCLOCK.tick(FPS)

# this calls the 'main' function when this script is executed
if __name__ == '__main__': main()


